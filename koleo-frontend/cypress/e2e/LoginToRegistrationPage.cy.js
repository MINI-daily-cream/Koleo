describe("AccountPage", () => {
  beforeEach(() => {
    cy.visit(Cypress.config().baseUrl + "/account");
  });

  it("should redirect to register page when register button is clicked", () => {
    cy.contains("Zarejestruj się").click();

    cy.url().should("eq", Cypress.config().baseUrl + "/register");
  });
});
