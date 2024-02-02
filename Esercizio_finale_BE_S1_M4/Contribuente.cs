using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Esercizio_finale_BE_S1_M4
{
    public class Contribuente
    {
        private static int scelta;
        private static List<Contribuente> listaContribuenti = new List<Contribuente>();
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string DataNascita { get; set; }
        public string CodiceFiscale { get; set; }
        public string Sesso { get; set; }
        public string ComuneResidenza { get; set; }
        public double RedditoAnnuale { get; set; }
        public double ImpostaDovuta { get; set; }


        /*
          * Summary: ottiene dall'utente i dati di un nuovo contribuente dagli input
          * Parameters: nessun parametro d'ingresso
          * Return: un oggetto chiamato Contribuente contenente i dati inseriti
          * Extra: aggiunti 2 do while per sesso e reddito in modo da non avere dati errati e porre nuovamente la stessa domanda in caso di errore
        */
        public static Contribuente DatiContribuente()
        {
            Console.WriteLine("Inserire nome:");
            string inputNome = Console.ReadLine();
            Console.WriteLine("Inserire il cognome:");
            string inputCognome = Console.ReadLine();
            Console.WriteLine("Inserire la data di nascita:");
            string inputData = Console.ReadLine();

            // Ho creato il do while dopo essermi reso conto di voler dare una scelta più stringente e quindi in caso di risposte non in linea con la domanda la stessa viene posta nuovamente

            string inputSesso;
            do {
                Console.WriteLine("Inserire il sesso (M/F/Non binario):");
                inputSesso = Console.ReadLine().ToUpper();

                switch (inputSesso)
                {
                    case "M":
                    case "F":
                    case "NON BINARIO":
                        break;
                    default:
                        Console.WriteLine("\n" + "Scelta non valida. Inserire M per Maschio, F per Femmina o Non binario." + "\n" );
                        break;
                }
            } while (inputSesso != "M" && inputSesso != "F" && inputSesso != "NON BINARIO");


            Console.WriteLine("Inserire il codice fiscale:");
            string inputCodice = Console.ReadLine();
            Console.WriteLine("Inserire il comune di residenza:");
            string inputResidenza = Console.ReadLine();

            // Anche come per il do while sopra mi sono reso conto di volere una maggiore precisione nella risposta per questo in caso di un valore non numerico o negativo la domanda viene posta nuovamente

            double reddito;
            string inputReddito;
            do
            {
                Console.WriteLine("Inserire il reddito annuale:");
                inputReddito = Console.ReadLine();

                if (!double.TryParse(inputReddito, out reddito) || reddito < 0)
                {
                    Console.WriteLine("\n" + "Scelta non valida. Inserire un numero positivo." + "\n");
                }
            } while (reddito <= 0);

            Contribuente nuovoContribuente = new Contribuente
            {
                Nome = inputNome,
                Cognome = inputCognome,
                DataNascita = inputData,
                CodiceFiscale = inputCodice,
                Sesso = inputSesso,
                ComuneResidenza = inputResidenza,
                RedditoAnnuale = reddito
            };

            return nuovoContribuente;
        }

        /*
         * Summary: Mostra il menù, sia all'inizio, sia dopo che sono state eseguite le altre funzioni e procedure
         * Parameters: nessun parametro d'ingresso
         * Return: nessun valore di ritorno
         * Extra; aggiunte 2 voci per le aggiunte exrea compito
        */
        public static void Menu()
        {
            while (true)
            {   // Colore menù blu, mi sembrava interessante dargli un colore diverso
                Console.WriteLine("\n");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++");
                Console.WriteLine("                 M E N U                 ");
                Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++");
                Console.WriteLine("1) Inserimento di un nuovo contribuente");
                Console.WriteLine("2) Lista dei contribuenti ");
                Console.WriteLine("3) Modifica dati contribuente");
                Console.WriteLine("4) Media Redditi e Imposte Contribuenti");
                Console.WriteLine("5) Esci dal programma");
                Console.ResetColor();
                Console.WriteLine("\n" + "Scegli un opzione:");
                

                if (int.TryParse(Console.ReadLine(), out scelta))
                {
                    switch (scelta)
                    {
                        case 1:
                            Console.WriteLine("\n" + "Opzione 1: Inserimento di un nuovo contribuente");
                            Contribuente nuovoContribuente = DatiContribuente();
                            if (nuovoContribuente != null)
                            {
                                CalcolaImposta(nuovoContribuente);
                                listaContribuenti.Add(nuovoContribuente);
                                Console.WriteLine("\n" + "Contribuente aggiunto alla lista.");
                            }
                            Console.WriteLine("\n" +"Premere Invio per tornare al menu");
                            Console.ReadLine();
                            break;
                        case 2:
                            // Extra rispetto al compito, possibilità di vedere la lista dei contribuenti in ordine alfabetico per nome o cognome
                            Console.WriteLine("\n" + "Opzione 2: Lista Contribuenti");
                            ListaContribuenti();
                            Console.WriteLine("\n" + "Premere Invio per tornare al menu");
                            Console.ReadLine();
                            break;        
                        case 3:
                            // Extra rispetto al compito, da la possibilità di cercare i contribuenti e poi modificarli e cancellarli (tramite ModificaContribuente), aggiunta per la ricerca il ToLower altrimenti non avrebbe avuto riscontro con maiuscole e minuscole diverse
                            Console.WriteLine("\n" + "Opzione 3: Modifica Contribuente");
                            Console.WriteLine("Inserire il codice fiscale del contribuente da modificare:");
                            string codiceFiscaleModifica = Console.ReadLine().ToLower();
                            Contribuente contribuenteDaModificare = listaContribuenti.Find(c => c.CodiceFiscale.ToLower() == codiceFiscaleModifica);
                            if (contribuenteDaModificare != null)
                            {
                                contribuenteDaModificare.ModificaContribuente();
                                CalcolaImposta(contribuenteDaModificare);
                                Console.WriteLine("\n" + "Contribuente modificato con successo.");
                            }
                            else
                            {
                                Console.WriteLine("\n" + "Contribuente non trovato.");
                            }
                            Console.WriteLine("\n" + "Premere Invio per tornare al menu");
                            Console.ReadLine();
                            break;
                        case 4:
                            // Extra rispetto al compito, da la possibilità di vedere le medie dei redditi, delle imposte dovute e il numero di contribuenti (tramite CalcolaMedia)
                            Console.WriteLine("\n" + "Opzione 4: Media Redditi e Imposte Contribuenti");
                            CalcolaMedie();
                            Console.WriteLine("\n" + "Premere Invio per tornare al menu");
                            Console.ReadLine();
                            break;
                        case 5:
                            // aggiunta conferma d'uscita dall'applicazione 
                            Console.WriteLine("\n" + "Opzione 5: Uscita dal programma." + "\n");
                            bool rispostaValida = false;
                            while (!rispostaValida)
                            {
                                Console.WriteLine("Sei sicuro di voler uscire ?");
                                Console.WriteLine("1) Si");
                                Console.WriteLine("2) No");
                                string esci = Console.ReadLine();

                                switch (esci.ToLower())
                                {
                                    case "1":
                                    case "si":
                                        return;
                                    case "2":
                                    case "no":
                                        rispostaValida = true;
                                        break;
                                    default:
                                        Console.WriteLine("Opzione non valida, riprovare" + "\n");
                                        break;
                                }
                            }
                            break;

                        default:
                            
                            Console.WriteLine("\n" + "Opzione non valida, riprovare" + "\n");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Opzione non valida, riprovare ");
                }
            }

        }

        /*
         * Summary: calcola l'imposta dovuta per un contribuente in base al suo reddito 
         * Parameters: riceve un oggetto chiamato Contribuente che viene creato nella funzione DatiContribuente
         * Return: nessun valore di ritorno
         * Extra: utilizzato if else perchè con switch creava problemi 
        */
        public static void CalcolaImposta(Contribuente contribuente)
        {
            double reddito = contribuente.RedditoAnnuale;

            if (reddito <= 15000)
            {
                contribuente.ImpostaDovuta = reddito * 0.23;
            }
            else if (reddito <= 28000)
            {
                contribuente.ImpostaDovuta = 3450 + (reddito - 15000) * 0.27;
            }
            else if (reddito <= 55000)
            {
                contribuente.ImpostaDovuta = 6960 + (reddito - 28000) * 0.38;
            }
            else if (reddito <= 75000)
            {
                contribuente.ImpostaDovuta = 17220 + (reddito - 55000) * 0.41;
            }
            else
            {
                contribuente.ImpostaDovuta = 25420 + (reddito - 75000) * 0.43;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n" + $"CALCOLO DELL’IMPOSTA DA VERSARE:");
            Console.WriteLine($"Contribuente: {contribuente.Nome} {contribuente.Cognome},");
            Console.WriteLine($"Nato il {contribuente.DataNascita} ({contribuente.Sesso}),");
            Console.WriteLine($"Residente in {contribuente.ComuneResidenza},");
            Console.WriteLine($"Codice fiscale: {contribuente.CodiceFiscale}");
            Console.WriteLine($"Reddito dichiarato: EURO {contribuente.RedditoAnnuale}");
            Console.ForegroundColor = ConsoleColor.Red;  
            Console.WriteLine($"IMPOSTA DA VERSARE: EURO {contribuente.ImpostaDovuta}");                                           // colore imposta da versare messo in rosso
            Console.ResetColor();

        }
        /* Aggiunta Extra rispetto alla consegna
          
           * Summary: mostra la lista dei contribuenti con tutti i dati e l'imposta dovuta che è stata calcolata precedentemente
           * Parameters: nessun parametro d'ingresso
           * Return: nessun valore di ritorno
           * Extra: aggiunta possibilità di ordinare alfabeticamente per nome o cognome
        */
        public static void ListaContribuenti()
        {
            Console.WriteLine("Lista dei contribuenti:");
            // Qui il codice per ordinare i contribuenti in ordine alfabetico per nome o cognome
            Console.WriteLine("\n" + "Ordina per:");
            Console.WriteLine("1) Nome");
            Console.WriteLine("2) Cognome" + "\n");

            if (int.TryParse(Console.ReadLine(), out int scelta))
            {
                switch (scelta)
                {
                    case 1:
                        listaContribuenti.Sort((x, y) => string.Compare(x.Nome, y.Nome, StringComparison.OrdinalIgnoreCase));
                        foreach (var contribuente in listaContribuenti)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\n" + $"Contribuente: {contribuente.Nome} {contribuente.Cognome},");
                            Console.WriteLine($"Nato il {contribuente.DataNascita} ({contribuente.Sesso}),");
                            Console.WriteLine($"Residente in {contribuente.ComuneResidenza},");
                            Console.WriteLine($"Codice fiscale: {contribuente.CodiceFiscale}");
                            Console.WriteLine($"Reddito dichiarato: EURO {contribuente.RedditoAnnuale}");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"IMPOSTA DA VERSARE: EURO {contribuente.ImpostaDovuta}");                                       // colore imposta da versare messo in rosso    
                            Console.ResetColor();
                        }
                        break;
                    case 2:
                        listaContribuenti.Sort((x, y) => string.Compare(x.Cognome, y.Cognome, StringComparison.OrdinalIgnoreCase));
                        foreach (var contribuente in listaContribuenti)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\n" + $"Contribuente: {contribuente.Nome} {contribuente.Cognome},");
                            Console.WriteLine($"Nato il {contribuente.DataNascita} ({contribuente.Sesso}),");
                            Console.WriteLine($"Residente in {contribuente.ComuneResidenza},");
                            Console.WriteLine($"Codice fiscale: {contribuente.CodiceFiscale}");
                            Console.WriteLine($"Reddito dichiarato: EURO {contribuente.RedditoAnnuale}");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"IMPOSTA DA VERSARE: EURO {contribuente.ImpostaDovuta}");                                       // colore imposta da versare messo in rosso    
                            Console.ResetColor();
                        }
                        break;
                
                    default:
                        Console.WriteLine("Opzione non valida.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Input non valido. Inserire un numero.");
            }
           
        }

      /*
       * Aggiunta Extra rispetto alla consegna  
       
         * Summary: permette di modificare i dati di un contribuente cercandolo tramite codice fiscale, permette anche la cancellazione del contribuente
         * Parameters: nessun parametro in ingresso
         * Return: nessun valore di ritorno
         * Extra; aggiunti i 2 do while su Sesso e Reddito in modo da ripetere la domanda in caso di dati errati
      */
        public void ModificaContribuente()
        {
            // ho messo un riepilogo dei dati dell'utente, senza era difficile capire cosa anare a modificare 

            Console.WriteLine("Riepilogo dei dati attuali:");
            Console.WriteLine($"Nome: {Nome}");
            Console.WriteLine($"Cognome: {Cognome}");
            Console.WriteLine($"Data di nascita: {DataNascita}");
            Console.WriteLine($"Codice Fiscale: {CodiceFiscale}");
            Console.WriteLine($"Residenza: {ComuneResidenza}");
            Console.WriteLine($"Reddito: {RedditoAnnuale}");

            Console.WriteLine("\n" + "Modifica contribuente:");
            Console.WriteLine("1) Modifica Nome");
            Console.WriteLine("2) Modifica Cognome");
            Console.WriteLine("3) Modifica Data di nascita");
            Console.WriteLine("4) Modifica Codice Fiscale");
            Console.WriteLine("5) Modifica impostazioni Sesso");
            Console.WriteLine("6) Modifica Residenza");
            Console.WriteLine("7) Modifica Reddito");
            Console.WriteLine("8) Cancella Contribuente");


            int opzioneModifica;
            if (int.TryParse(Console.ReadLine(), out opzioneModifica))
            {
                switch (opzioneModifica)
                {
                    case 1:
                        Console.WriteLine("Nuovo Nome:");
                        Nome = Console.ReadLine();
                        break;
                    case 2:
                        Console.WriteLine("Nuovo Cognome:");
                        Cognome = Console.ReadLine();
                        break;
                    case 3:
                        Console.WriteLine("Nuova Data di nascita:");
                        DataNascita = Console.ReadLine();
                        break;
                    case 4:
                        Console.WriteLine("Nuovo Codice Fiscale:");
                        CodiceFiscale = Console.ReadLine();
                        break;
                    case 5:
                        // questo do while arriva come conseguenza del rispettivo modicato in DatiContribuente
                        string inputSesso;
                        do {
                            Console.WriteLine("Modifica impostazioni Sesso (M/F/Non binario):");
                            inputSesso = Console.ReadLine().ToUpper();

                            switch (inputSesso)
                            {
                                case "M":
                                case "F":
                                case "NON BINARIO":
                                    Sesso = inputSesso;
                                    break;
                                default:
                                    Console.WriteLine("Scelta non valida. Inserire M per Maschio, F per Femmina o Non binario.");
                                    break;
                            }
                        } while (inputSesso != "M" && inputSesso != "F" && inputSesso != "NON BINARIO");
                        break;
                    case 6:
                        Console.WriteLine("Nuova Residenza:");
                        ComuneResidenza = Console.ReadLine();
                        break;
                    case 7:
                        // anche questo do while arriva come conseguenza del rispettivo modificato sopra 
                        do {
                            Console.WriteLine("Nuovo Reddito:");
                            string inputReddito = Console.ReadLine();
                            if (double.TryParse(inputReddito, out double nuovoReddito) && nuovoReddito > 0)
                            {
                                RedditoAnnuale = nuovoReddito;
                                break; 
                            }
                            else
                            {
                                Console.WriteLine("Scelta non valida. Inserire un valore numerico positivo." + "\n");
                            }
                           } while (true);
                        break;
                    case 8:
                        Console.WriteLine("\n" + "Sei sicuro di voler cancellare il contribuente? (S/N)");
                        if (Console.ReadLine().ToUpper() == "S")
                        {
                            listaContribuenti.Remove(this);
                            Console.WriteLine("Contribuente cancellato con successo.");
                        }
                        else
                        {
                            Console.WriteLine("Cancellazione annullata.");
                        }
                        break;
                    default:
                        Console.WriteLine("Opzione non valida.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Scelta non valida. Inserire il numero di un opzione disponibile.");
            }
        }

        /*
             * Aggiunta Extra rispetto alla consegna  

             * Summary: ci permette di vedere le medie di reddito e di imposte da versare dei Contribuenti 
             * Parameters: nessun parametro in ingresso
             * Return: restituisce la media dei redditi, delle imposte e il numero di contribuenti
        */
        public static void CalcolaMedie()
        {
            if (listaContribuenti.Count == 0)
            {
                Console.WriteLine("Nessun contribuente presente nella lista.");
            }
            else
            {
                double sommaRedditi = 0;
                double sommaImposte = 0;

                foreach (var contribuente in listaContribuenti)
                {
                    sommaRedditi += contribuente.RedditoAnnuale;
                }

                double mediaRedditi = sommaRedditi / listaContribuenti.Count;

                foreach (var contribuente in listaContribuenti)
                {
                    sommaImposte += contribuente.ImpostaDovuta;
                }

                double mediaImposte = sommaImposte / listaContribuenti.Count;

                Console.WriteLine($"Numero dei contribuenti: {listaContribuenti.Count}");
                Console.WriteLine($"Media dei redditi: {mediaRedditi}");
                Console.WriteLine($"Media delle imposte dovute: {mediaImposte}");
            }
        }
    }
}
