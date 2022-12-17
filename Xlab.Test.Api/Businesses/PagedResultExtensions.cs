using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Xlab.Test.Domain.Businesses;

namespace Xlab.Test.Api.Businesses;

public record PaginationQueryParameters(int? PageId, [Range(1,100)]int? PageSize);

public static class PagedResultExtensions
{
    public static IActionResult PagedOk<TResource, TRepresentation>(
        this ControllerBase controller,
        IQueryable<TResource> data,
        Func<TResource, TRepresentation> convert,
        int pageNo, int pageSize) 
        where TResource : Business
    {
        var count = data.Count();

        controller.Response.Headers.Add("Link", GetHeaderLinkString(new PageLinkBuilder(controller.Url, pageNo, pageSize, count)));

        var pagedData = data.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
        return controller.Ok(pagedData.Select(convert).ToList());
    }

    private static string GetHeaderLinkString(PageLinkBuilder links)
    {
        var parts = new List<string>(4){ links.FirstPage + "; rel=first" };

        if (links.PreviousPage is { } previousPageLink)
        {
            parts.Add(previousPageLink + "; rel=prev");
        }
            
        if (links.NextPage is { } nextPageLink)
        {
            parts.Add(nextPageLink + "; rel=next");
        }
            
        parts.Add(links.LastPage + "; rel=last");

            
        return string.Join(", ", parts);
    }
}

public class PageLinkBuilder
{
    public string FirstPage { get; }
    public string LastPage { get; }
    public string? NextPage { get; }
    public string? PreviousPage { get; } 

    public PageLinkBuilder(IUrlHelper urlHelper, int pageNo, int pageSize, long totalRecordCount)
    {
        // Determine total number of pages
        var pageCount = totalRecordCount > 0
            ? (int) Math.Ceiling(totalRecordCount/(double) pageSize)
            : 0;

        // Create them page links 
        FirstPage = urlHelper.RouteUrl(new RouteValueDictionary
        {
            {"pageId", 1},
            {"pageSize", pageSize}
        }) ?? throw new InvalidOperationException("Unable to build URL");
        LastPage = urlHelper.RouteUrl(new RouteValueDictionary
        {
            {"pageId", pageCount > 1 ? pageCount : 1},
            {"pageSize", pageSize}
        }) ?? throw new InvalidOperationException("Unable to build URL");
        if (pageNo > 1)
        {
            PreviousPage = urlHelper.RouteUrl( new RouteValueDictionary
            {
                {"pageId", pageNo - 1},
                {"pageSize", pageSize}
            }) ?? throw new InvalidOperationException("Unable to build URL");
        }
        if (pageNo < pageCount)
        {
            NextPage = urlHelper.RouteUrl( new RouteValueDictionary
            {
                {"pageId", pageNo + 1},
                {"pageSize", pageSize}
            }) ?? throw new InvalidOperationException("Unable to build URL");
        }
    }
}