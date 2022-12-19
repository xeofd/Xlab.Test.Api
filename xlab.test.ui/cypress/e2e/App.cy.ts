/// <reference types="cypress" />

describe("app static sections", () => {
    beforeEach(() => {
        cy.visit("/");

        cy.intercept(
            {
                method: "GET",
                url: "/businesses*",
                hostname: "localhost",
            },
            { statusCode: 200, body: [] }
        ).as("apiSearch");
    });

    it("should have page title", () => {
        cy.get("[data-cy=app-title]").should(
            "have.text",
            "XLab Techtest search"
        );
    });

    it("should have search bar", () => {
        const searchbox = cy.get("[data-cy=app-searchbar]");

        searchbox.should("exist");
        searchbox.get("input").clear().type("search term");

        searchbox.get("input").should("have.value", "search term");
    });

    it("should call api when typing in search bar", () => {
        cy.get("[data-cy=app-searchbar]").get("input").clear().type("search");

        cy.wait("@apiSearch").get("@apiSearch.all").should("have.length", 8); // 8 as this will get called (twice because of strict) on page load as well
    });
});

describe("app data section when data is loaded", () => {
    beforeEach(() => {
        cy.visit("/");
    });

    it("should display businesses when API returns them", () => {
        const business = {
            id: "id",
            name: "name",
            category: "category",
            excerpt: "excerpt",
            thumbnail: "http://localhost",
            phone: "phone",
            address: "adress",
            tags: ["tag1", "tag2"],
        };

        cy.intercept(
            {
                method: "GET",
                url: "/businesses*",
                hostname: "localhost",
            },
            {
                statusCode: 200,
                body: [business],
            }
        ).as("apiSearch");

        cy.get(`[data-cy=business-thumbnail-${business.id}`).should("exist");
        cy.get(`[data-cy=business-name-${business.id}`).should("have.text", business.name);
        cy.get(`[data-cy=business-contact-${business.id}`).should("have.text", `${business.phone} - ${business.address}`);
        cy.get(`[data-cy=business-excerpt-${business.id}`).should("have.text", business.excerpt);
        cy.get(`[data-cy=business-tag-${business.id}-0`).should("have.text", business.tags[0]);
        cy.get(`[data-cy=business-tag-${business.id}-1`).should("have.text", business.tags[1]);
    });

    it("should display no data message when API returns no businesses", () => {
        cy.intercept(
            {
                method: "GET",
                url: "/businesses*",
                hostname: "localhost",
            },
            {
                statusCode: 200,
                body: [],
            }
        ).as("apiSearch");

        cy.get("[data-cy=app-nodata]").should(
            "contain.text",
            "No businesses found"
        );
    });
});
