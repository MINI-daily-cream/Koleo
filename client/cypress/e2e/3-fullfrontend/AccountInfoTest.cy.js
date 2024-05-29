describe("User Login", () => {
  it('should log in the user and display user data after clicking "Dane Użytkownika"', () => {
    cy.visit("/");

    cy.get("#email").type("test@example.com");
    cy.get("#password").type("password123");
    cy.contains("button", "Zaloguj się").click();

    cy.url().should("include", "/account");

    cy.contains("Dane użytkownika").click();

    cy.contains("test@example.com").should("exist");
  });
});
