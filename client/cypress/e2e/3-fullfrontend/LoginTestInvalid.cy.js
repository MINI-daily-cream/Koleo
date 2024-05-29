describe("User Login", () => {
  it("should show an error message with incorrect login credentials", () => {
    cy.visit("/");

    cy.get("#email").type("wrong@example.com");

    cy.get("#password").type("wrongpassword");

    cy.contains("button", "Zaloguj się").click();

    cy.contains("Błędny adres email lub hasło").should("exist");
  });
});
