describe("Change Password Page", () => {
  it("should change the password successfully", () => {
    cy.visit("/");

    cy.get("#email").type("test@example.com");
    cy.get("#password").type("password123");
    cy.contains("button", "Zaloguj się").click();
    cy.url().should("include", "/account");

    cy.contains("Dane użytkownika").click();

    cy.contains("Zmień hasło").click();

    cy.get('input[id="oldpassword"]').type("password123");

    cy.get('input[id="newpassword"]').type("password123");

    cy.contains("button", "Zatwierdź").click();

    cy.on("window:alert", (message) => {
      expect(message).to.equal("Hasło zostało pomyślnie zmienione");
    });
  });
});
