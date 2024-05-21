describe("User Login and Account Actions", () => {
  it('should log in the user, display user data, and click the "Zmień email" button', () => {
    cy.visit("/");

    cy.get("#email").type("test@example.com");
    cy.get("#password").type("password123");
    cy.contains("button", "Zaloguj się").click();

    cy.url().should("include", "/account");

    cy.contains("Dane użytkownika").click();

    cy.contains("Zmień email").click();

    cy.get('input[type="email"]').type("new-email@example.com");

    cy.contains("button", "Zatwierdź").click();

    cy.url().should("include", "/account/info");

    cy.contains("new-email@example.com").should("exist");

    cy.contains("Zmień email").click();

    cy.get('input[type="email"]').type("test@example.com");

    cy.contains("button", "Zatwierdź").click();
  });
});
