describe("User Registration", () => {
  it("should navigate to the registration page from the homepage and fill out the form", () => {
    cy.visit("/");

    cy.contains("Zarejestruj się").click();

    cy.url().should("include", "/register");

    cy.get("#email").type("test@example.com");
    cy.get("#password").type("password123");
    cy.get("#confirmPassword").type("password123");

    cy.contains("button", "Zarejestruj się").click();

    cy.url().should("include", "/account");
  });
});
