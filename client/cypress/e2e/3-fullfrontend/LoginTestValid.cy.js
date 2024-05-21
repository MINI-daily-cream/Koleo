describe("User Login", () => {
  it("should log in the user and redirect to the account page", () => {
    cy.visit("/");

    cy.get("#email").type("test@example.com");

    cy.get("#password").type("password123");

    cy.contains("button", "Zaloguj się").click();

    cy.url().should("include", "/account");

    cy.contains("Moje Konto").should("exist");
  });
});
