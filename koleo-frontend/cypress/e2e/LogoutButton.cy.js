describe("Logout", () => {
  it("should redirect to home page when logout button is clicked", () => {
    cy.visit(Cypress.config().baseUrl + "/account");

    cy.contains("Wyloguj się").click();

    cy.url().should("eq", Cypress.config().baseUrl + "/");
  });
});
