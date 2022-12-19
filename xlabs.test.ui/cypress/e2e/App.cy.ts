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
        );
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
});
