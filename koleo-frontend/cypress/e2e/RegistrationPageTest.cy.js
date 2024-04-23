describe("RegistrationPage", () => {
  beforeEach(() => {
    cy.visit(Cypress.config().baseUrl + "/Register");
  });

  it("should allow user to register with valid credentials", () => {
    const email = "test@example.com";
    const password = "Test123";

    cy.get('input[type="email"]').type(email);

    cy.get('input[type="password"]').eq(0).type(password);

    cy.get('input[type="password"]').eq(1).type(password);

    cy.get('button[type="submit"]').click();

    cy.url().should("eq", Cypress.config().baseUrl + "/account");
  });

  //it("should display error message when passwords do not match", () => {
  //  const email = "test@example.com";
  //  const password = "Test123";
  //  const invalidPassword = "DifferentPassword";

  //  cy.get('input[type="email"]').type(email);

  //  cy.get('input[type="password"]').eq(0).type(password);

  //  cy.get('input[type="password"]').eq(1).type(invalidPassword);

  //  cy.get('button[type="submit"]').click();

  //  cy.get(".password-error").should("be.visible");
  //  cy.get(".password-error").should(
  //    "contain.text",
  //    "Hasła nie pasują do siebie"
  //  );
  //});
});
