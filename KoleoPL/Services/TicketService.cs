using System;

namespace KoleoPL.Services
{
    public class TicketService
    {
        public void Buy() // pozwala na zakup biletu
        {
            PaymentService paymentService = new PaymentService(); // potem będzie jako dependency
            // (dependency injection w konstruktorze)
            paymentService.ProceedPayment();
        }

        public void ListByUser() // umożliwia ka˙zdemu u˙zytkownikowi wgl ˛ad w jego bilety
        {
        }

        public void Generate() // tworzy plik pdf z biletem
        {
            
        }

        public void Remove() // usuwa bilet z bazy danych i ponownie ł ˛aczy si˛e z PaymentService w celu zwrócenia pieni˛edzy
        // potrzeba dodatkowej metody w PaymentService ?
        {
            PaymentService paymentService = new PaymentService(); // potem będzie jako dependency
            // (dependency injection w konstruktorze)
            paymentService.ProceedPayment();
        }

        public void Add() // jest wywoływana przez Buy() i dodaje bilet do bazy danych
        {

        }

        public void ChangeDetails() // pozwala dokonac zmiany danych na bilecie i ponownie korzysta z metody Generate()
        {

        }
    }
}
