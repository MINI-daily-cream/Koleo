describe("Navbar", () => {
  beforeEach(() => {
    cy.visit(Cypress.config().baseUrl + "/Register");
  });

  it("should navigate to home page when user icon is clicked", () => {
    cy.get('[data-icon="house"]').click();

    cy.url().should("eq", Cypress.config().baseUrl + "/");
  });

  it("should navigate to account page when home icon is clicked", () => {
    cy.get('[data-icon="user"]').click();

    cy.url().should("eq", Cypress.config().baseUrl + "/account");
  });
});
