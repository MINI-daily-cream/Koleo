describe("LoginPage", () => {
  it("should redirect to account page when login button is clicked", () => {
    cy.visit(Cypress.config().baseUrl + "/login");

    cy.get("button").contains("Zaloguj się").click();

    cy.url().should("eq", Cypress.config().baseUrl + "/account");
  });
});
