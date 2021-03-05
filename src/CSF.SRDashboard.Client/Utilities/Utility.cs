using CSF.Components.Table.Entities;
using CSF.SRDashboard.Client.Models;
using CSF.SRDashboard.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.Utilities
{
    public class Utility
    {
        IMtoaArtifactService _artifactService;
        public Utility(IMtoaArtifactService artifactService)
        {
            _artifactService = artifactService;
        }


        public int FillDataForGrids(ref List<DashboardRow> newRequests,
                                    ref List<DashboardRow> requestsInProgress,
                                    ref List<DashboardRow> requestsOnHold,
                                    ref List<DashboardRow> requestsCompleted,
                                    ref List<DashboardRow> requestsNotSubmitted)
        {

            var allRequests = this._artifactService.GetDashboardRowsInSequence();
            allRequests = this.AddMockServiceStandardAndFullName(allRequests);

            newRequests = allRequests.Where(x => x.ProcessingPhase == Constants.New).ToList();
            requestsInProgress = allRequests.Where(x => x.ProcessingPhase == Constants.InProgress).ToList();
            requestsOnHold = allRequests.Where(x => x.ProcessingPhase == Constants.OnHold).ToList();
            requestsCompleted = allRequests.Where(x => x.ProcessingPhase == Constants.Completed).ToList();
            requestsNotSubmitted = allRequests.Where(x => x.ProcessingPhase == Constants.NotSubmitted).ToList();

            int numberOfTotalRequests = allRequests.Count;
            return numberOfTotalRequests;
        }

        public RequestGridsModel FillDataForGrids(RequestGridsModel requestGridsModel)
        {
            var allRequests = this._artifactService.GetDashboardRowsInParallel();
            allRequests = this.AddMockServiceStandardAndFullName(allRequests);
            requestGridsModel.NewRequests = allRequests.Where(x => x.ProcessingPhase == Constants.New).ToList();

            requestGridsModel.RequestsInProgress = allRequests.Where(x => x.ProcessingPhase == Constants.InProgress).ToList();

            requestGridsModel.RequestsOnHold = allRequests.Where(x => x.ProcessingPhase == Constants.OnHold).ToList();

            requestGridsModel.RequestsCompleted = allRequests.Where(x => x.ProcessingPhase == Constants.Completed).ToList();

            requestGridsModel.RequestsNotSubmitted = allRequests.Where(x => x.ProcessingPhase == Constants.NotSubmitted).ToList();

            return requestGridsModel;
        }


        // this is a temporary method that we want to use while we do not have Service standard and the applicant full name data
        private List<DashboardRow> AddMockServiceStandardAndFullName(List<DashboardRow> rows)
        {
            Random random = new Random();
            List<string> randomNames = new List<string>() { "Joe Smith",
                                                            "Sophia-Rose Bush",
                                                            "Shabaz Zavala",
                                                            "Zach Sheppard",
                                                            "Kierran Johnston",
                                                            "Aliya Dolan",
                                                            "Melisa Hamilton",
                                                            "Lottie Nixon",
                                                            "Kacey Rios",
                                                            "Adem Giles",
                                                            "Isla-Mae Bellamy",
                                                            "Nikki Odom",
                                                            "Samirah Burrows",
                                                            "Alice Gordon",
                                                            "Lavinia Olsen",
                                                            "Tom Braun",
                                                            "Yosef Molina",
                                                            "Nabila Harris",
                                                            "Hasan Forster",
                                                            "Kester Gale",
                                                            "Henna Mckee",
                                                            "Anne-Marie Howell",
                                                            "Anastazja Mcguire",
                                                            "Lorelei Bowes",
                                                            "Alvin Kaye",
                                                            "Ronnie Hobbs",
                                                            "Umaima Whelan",
                                                            "Jannat Dalby",
                                                            "Landon Ferreira",
                                                            "Caine Haigh",
                                                            "Emilio Trujillo",
                                                            "Humera Willis",
                                                            "Omari Connelly",
                                                            "Yousaf Jeffery",
                                                            "Nyle Shields",
                                                            "Vinnie Hatfield",
                                                            "Humaira Davey",
                                                            "Marguerite Hough",
                                                            "Abbie Mason",
                                                            "Luella Chaney",
                                                            "Kaiden Rocha",
                                                            "Angelica Cisneros",
                                                            "Charlton Mathis",
                                                            "Greyson Monroe"};

            foreach (var row in rows)
            {
                row.ServiceStandard = random.Next(1, 22);
                int index = random.Next(0, randomNames.Count);
                row.FullName = randomNames[index];
            }

            return rows;
        }

        //Use following two methods for filling mock data when not getting data from MTOA
        public List<DashboardRow> FillMockRows()
        {
            List<DashboardRow> requestList = new List<DashboardRow>();

            List<string> randomNames = new List<string>() { "Joe Smith",
                                                            "Sophia-Rose Bush",
                                                            "Shabaz Zavala",
                                                            "Zach Sheppard",
                                                            "Kierran Johnston",
                                                            "Aliya Dolan",
                                                            "Kurt Bassett",
                                                            "Lilly-May Larson",
                                                            "Ellie-May Murray",
                                                            "Jasmin Bradshaw",
                                                            "Matei Hackett",
                                                            "Fredrick Mahoney",
                                                            "Miles Boyer",
                                                            "Nikodem Cairns",
                                                            "Suman Metcalfe",
                                                            "Hari Shepherd",
                                                            "Krystal Sparks",
                                                            "Melisa Hamilton",
                                                            "Lottie Nixon",
                                                            "Kacey Rios",
                                                            "Adem Giles",
                                                            "Isla-Mae Bellamy",
                                                            "Nikki Odom",
                                                            "Samirah Burrows",
                                                            "Alice Gordon",
                                                            "Lavinia Olsen",
                                                            "Tom Braun",
                                                            "Yosef Molina",
                                                            "Nabila Harris",
                                                            "Hasan Forster",
                                                            "Kester Gale",
                                                            "Henna Mckee",
                                                            "Anne-Marie Howell",
                                                            "Anastazja Mcguire",
                                                            "Lorelei Bowes",
                                                            "Alvin Kaye",
                                                            "Ronnie Hobbs",
                                                            "Umaima Whelan",
                                                            "Jannat Dalby",
                                                            "Hamaad Robinson",
                                                            "Shaunna Humphrey",
                                                            "Farhaan Conley",
                                                            "Bryan Wardle",
                                                            "Brandon Carpenter",
                                                            "Leigha Garrison",
                                                            "Jay Felix",
                                                            "Landon Ferreira",
                                                            "Caine Haigh",
                                                            "Emilio Trujillo",
                                                            "Humera Willis",
                                                            "Saara Krueger",
                                                            "Roma Sweeney",
                                                            "Malak Macleod",
                                                            "Nellie Southern",
                                                            "Marcie Noble",
                                                            "Huzaifah Curtis",
                                                            "Kendall Forrest",
                                                            "Jayden-Lee Almond",
                                                            "Clifford Hartley",
                                                            "Shae Rayner",
                                                            "Charlotte Traynor",
                                                            "Sofia Leon",
                                                            "Ibrar Saunders",
                                                            "Omari Connelly",
                                                            "Yousaf Jeffery",
                                                            "Nyle Shields",
                                                            "Vinnie Hatfield",
                                                            "Humaira Davey",
                                                            "Marguerite Hough",
                                                            "Abbie Mason",
                                                            "Luella Chaney",
                                                            "Teigan Hail",
                                                            "Renzo Strong",
                                                            "Yahya Sutton",
                                                            "Elwood Kaur",
                                                            "Lily-Grace Arias",
                                                            "Franco Owen",
                                                            "Kaiden Rocha",
                                                            "Angelica Cisneros",
                                                            "Nico Hall",
                                                            "Isaac Shea",
                                                            "Barbara Britt",
                                                            "Akbar Tierney",
                                                            "Lillia Sosa",
                                                            "Agatha Andersen",
                                                            "Maude John",
                                                            "Tayla Mccallum",
                                                            "Aairah Perry",
                                                            "Rory Greer",
                                                            "Komal East",
                                                            "Shyam Porter",
                                                            "Brendon Torres",
                                                            "Velma Sanderson",
                                                            "Cleveland Kemp",
                                                            "Patrick Moody",
                                                            "Tyron Downes",
                                                            "Ruby-Rose Wheatley",
                                                            "Adnaan Figueroa",
                                                            "Terence Miles",
                                                            "Charlton Mathis",
                                                            "Greyson Monroe"};
            List<string> randomStaffs = new List<string>() { "Ida Ghahremani", "Kay Nadalin ", "Samir Sheikh ", "Paul Delannoy", "Colin Gunanayagam", "Tibo Phung" };

            List<string> processingPhases = new List<string>() { "New", "In Progress", "On Hold", "Completed" };

            string[,] certificates =
            {
                    { "0", "   ", "     " },
                    { "1", "2010-M500-NC-LCWV.", "CAPITAINE, JAUGE BRUTE 500, À PROXIMITÉ DU LITTORAL (M500-NC-LCWV)." },
                    { "2", "ABLE SEAFARER DECK", "MARIN QUALIFIÉ PONT" },
                    { "3", "ADVANCED FIRE FIGHTING", "TECHNIQUES AVANCÉES DE LUTTE CONTRE L'INCENDIE" },
                    { "4", "ADVANCED TRAINING FOR CHEMICAL TANKER CARGO OPERATIONS", "FORMATION AVANCÉE AUX OPÉRATIONS LIÉES À LA CARGAISON DES NAVIRES-CITERNES POUR PRODUITS CHIMIQUES" },
                    { "7", "ADVANCED TRAINING FOR LIQUEFIED GAS TANKER CARGO OPERATIONS", "FORMATION AVANCÉE AUX OPÉRATIONS LIÉES À LA CARGAISON DES NAVIRES-CITERNES POUR GAZ LIQUÉFIÉS" },
                    { "10", "ADVANCED TRAINING FOR OIL TANKER CARGO OPERATIONS", "FORMATION AVANCÉE AUX OPÉRATIONS LIÉES À LA CARGAISON DES PÉTROLIERS" },
                    { "13", "ADVANCED TRAINING FOR PERSONNEL ON SHIPS OPERATING IN POLAR WATERS", "FORMATION AVANCÉE POUR LE PERSONNEL DE BÂTIMENTS NAVIGUANT DANS LES EAUX POLAIRES" },
                    { "14", "Advanced Training For Personnel On Ships Operating In Polar Waters ", "Formation avancée pour le personnel de bâtiments naviguant dans les eaux polaires " },
                    { "15", "BASIC TRAINING FOR LIQUEFIED GAS TANKER CARGO OPERATIONS - OFFICER", "FORMATION DE BASE AUX OPÉRATIONS LIÉES À LA CARGAISON DES NAVIRES-CITERNES POUR GAZ LIQUÉFIÉS - OFFICIER" },
                    { "18", "BASIC TRAINING FOR LIQUEFIED GAS TANKER CARGO OPERATIONS - RATING", "FORMATION DE BASE AUX OPÉRATIONS LIÉES À LA CARGAISON DES NAVIRES-CITERNES POUR GAZ LIQUÉFIÉS - MATELOT" },
                    { "21", "BASIC TRAINING FOR OIL AND CHEMICAL TANKER CARGO OPERATIONS - OFFICER", "FORMATION DE BASE AUX OPÉRATIONS LIÉES À LA CARGAISON DES PÉTROLIERS ET DES NAVIRES-CITERNES POUR PRODUITS CHIMIQUES - OFFICIER" },
                    { "24", "BASIC TRAINING FOR OIL AND CHEMICAL TANKER CARGO OPERATIONS - RATING", "FORMATION DE BASE AUX OPÉRATIONS LIÉES À LA CARGAISON DES PÉTROLIERS ET DES NAVIRES-CITERNES POUR PRODUITS CHIMIQUES - MATELOT" },
                    { "27", "BASIC TRAINING FOR PERSONNEL ON SHIPS IN POLAR WATERS", "FORMATION DE BASE POUR LE PERSONNEL DE BATIMENTS NAVIGUANT DANS LES EAUX POLAIRES " },
                    { "28", "BRIDGE WATCH RATING", "MATELOT DE QUART À LA PASSERELLE" },
                    { "29", "Basic Training For Personnel On Ships Operating In Polar Waters ", "Formation de base pour le personnel de bâtiments naviguant dans les eaux polaires" },
                    { "30", "CERTIFICATE OF PROFICIENCY IN ADVANCED TRAINING FOR PERSONNEL ON SHIPS OPERATING IN POLAR WATERS ", "CERTIFICAT D'APTITUDE EN FORMATION AVANCÉE POUR LE PERSONNEL DE BATIMENTS NAVIGUANT DANS LES EAUX POLAIRES  " },
                    { "31", "CERTIFICATE OF PROFICIENCY IN BASIC TRAINING FOR PERSONNEL ON SHIPS OPERATING IN POLAR WATERS", "CERTIFICAT D'APTITUDE EN FORMATION DE BASE POUR LE PERSONNEL DE BATIMENT NAVIGUANT DANS LES EAUX POLAIRES " },
                    { "32", "CHIEF ENGINEER, MOTORSHIP", "CHEF MÉCANICIEN, NAVIRE À MOTEUR" },
                    { "33", "CHIEF ENGINEER, STEAMSHIP", "CHEF MÉCANICIEN, NAVIRE À VAPEUR" },
                    { "34", "CHIEF MATE", "PREMIER OFFICIER DE PONT" },
                    { "35", "CHIEF MATE, NEAR COASTAL", "PREMIER OFFICIER DE PONT, À PROXIMITÉ DU LITTORAL" },
                    { "36", "CHIEF MATE.", "PREMIER OFFICIER DE PONT." },
                    { "37", "Certificate Of Proficiency In Advanced Training For Personnel On Ships Operating In Polar Waters ", "Certificat d’aptitude en formation avancée pour le personnel de bâtiments naviguant dans les eaux polaires " },
                    { "38", "Certificate Of Proficiency In Basic Training For Personnel On Ships Operating In Polar Waters ", "Certificat d’aptitude en formation de base pour le personnel de bâtiments naviguant dans les eaux polaires " },
                    { "39", "ENGINE-ROOM RATING", "MATELOT DE LA SALLE DES MACHINES" },
                    { "40", "FAST RESCUE BOATS", "EXPLOITATION DES CANOTS DE SECOURS RAPIDES" },
                    { "43", "FIRST MATE LONG RUN FERRY", "PREMIER LIEUTENANT DE TRANSBORDEUR À TRAJET LONG" },
                    { "44", "FIRST-CLASS ENGINEER, MOTOR SHIP", "OFFICIER MÉCANICIEN DE PREMIÈRE CLASSE, NAVIRE À MOTEUR" },
                    { "45", "FIRST-CLASS ENGINEER, MOTOR SHIP AND STEAMSHIP", "OFFICIER MÉCANICIEN DE PREMIÈRE CLASSE, NAVIRE À MOTEUR ET NAVIRE À VAPEUR" },
                    { "46", "FIRST-CLASS ENGINEER, MOTOR SHIP FOURTH-CLASS ENGINEER, STEAMSHIP", "OFFICIER MÉCANICIEN DE PREMIÈRE CLASSE, NAVIRE À MOTEUR OFFICIER MÉCANICIEN DE  QUATRIÈME CLASSE, NAVIRE À VAPEUR" },
                    { "47", "FIRST-CLASS ENGINEER, MOTOR SHIP SECOND-CLASS ENGINEER, STEAMSHIP", "OFFICIER MÉCANICIEN DE PREMIÈRE CLASSE, NAVIRE À MOTEUR  OFFICIER MÉCANICIEN DE DEUXIÈME CLASSE, NAVIRE À VAPEUR " },
                    { "48", "FIRST-CLASS ENGINEER, MOTOR SHIP THIRD-CLASS ENGINEER, STEAMSHIP", "OFFICIER MÉCANICIEN DE PREMIÈRE CLASSE, NAVIRE À MOTEUR  OFFICIER MÉCANICIEN DE TROISIÈME CLASSE, NAVIRE À VAPEUR" },
                    { "49", "FIRST-CLASS ENGINEER, STEAMSHIP", "OFFICIER MÉCANICIEN DE PREMIÈRE CLASSE, NAVIRE À VAPEUR" },
                    { "50", "FOURTH-CLASS ENGINEER, MOTOR SHIP", "OFFICIER MÉCANICIEN DE QUATRIÈME CLASSE, NAVIRE À MOTEUR" },
                    { "51", "FOURTH-CLASS ENGINEER, MOTOR SHIP  AND STEAMSHIP", "OFFICIER MÉCANICIEN DE QUATRIÈME CLASSE, NAVIRE À MOTEUR ET NAVIRE À VAPEUR" },
                    { "52", "FOURTH-CLASS ENGINEER, STEAMSHIP", "OFFICIER MÉCANICIEN DE QUATRIÈME CLASSE, NAVIRE À VAPEUR" },
                    { "53", "MARINE ADVANCED FIRST AID", "SECOURISME AVANCÉ EN MER" },
                    { "54", "MARINE MEDICAL CARE", "SOINS MÉDICAUX EN MER" },
                    { "55", "MASTER 3000 GROSS TONNAGE, NEAR COASTAL", "CAPITAINE, BÂTIMENT D'UNE JAUGE BRUTE DE 3000, À PROXIMITÉ DU LITTORAL" },
                    { "56", "MASTER 500 GROSS TONNAGE, NEAR COASTAL (M500-NC)", "CAPITAINE, BÂTIMENT D'UNE JAUGE BRUTE DE 500, À PROXIMITÉ DU LITTORAL (M500-NC)" },
                    { "57", "MASTER 500 GROSS TONNAGE, NEAR COASTAL (M500-NC-LCWV)", "CAPITAINE, JAUGE BRUTE 500, À PROXIMITÉ DU LITTORAL (M500-NC-LCWV)" },
                    { "58", "MASTER 500 GROSS TONNAGE, NEAR COASTAL (M500-NC-TNC2)", "CAPITAINE, BÂTIMENT D'UNE JAUGE BRUTE DE 500, À PROXIMITÉ DU LITTORAL (M500-NC-TNC2)" },
                    { "59", "MASTER 500 GROSS TONNAGE, NEAR COASTAL (M500-T3000)", "CAPITAINE, BÂTIMENT D'UNE JAUGE BRUTE DE 500, À PROXIMITÉ DU LITTORAL (M500-T3000)" },
                    { "60", "MASTER MARINER", "CAPITAINE AU LONG COURS" },
                    { "61", "MASTER NEAR COASTAL", "CAPITAINE À PROXIMITÉ DU LITTORAL" },
                    { "62", "PASSENGER SAFETY MANAGEMENT", "GESTION DE LA SÉCURITÉ DES PASSAGERS" },
                    { "63", "SECOND ENGINEER, MOTORSHIP", "MÉCANICIEN EN SECOND, NAVIRE À MOTEUR" },
                    { "64", "SECOND ENGINEER, STEAMSHIP", "MÉCANICIEN EN SECOND, NAVIRE À VAPEUR" },
                    { "65", "SECOND-CLASS ENGINEER, MOTOR SHIP", "OFFICIER MÉCANICIEN DE DEUXIÈME CLASSE, NAVIRE À MOTEUR" },
                    { "66", "SECOND-CLASS ENGINEER, MOTOR SHIP AND STEAMSHIP", "OFFICIER MÉCANICIEN DE DEUXIÈME CLASSE, NAVIRE À MOTEUR ET NAVIRE À VAPEUR" },
                    { "67", "SECOND-CLASS ENGINEER, MOTOR SHIP FOURTH-CLASS ENGINEER, STEAMSHIP", "OFFICIER MÉCANICIEN DE DEUXIÈME CLASSE, NAVIRE À MOTEUR OFFICIER MÉCANICIEN DE QUATRIÈME CLASSE, NAVIRE À VAPEUR" },
                    { "68", "SECOND-CLASS ENGINEER, MOTOR SHIP THIRD-CLASS ENGINEER, STEAMSHIP", "OFFICIER MÉCANICIEN DE DEUXIÈME CLASSE, NAVIRE À MOTEUR OFFICIER MÉCANICIEN DE TROISIÉME CLASSE, NAVIRE À VAPEUR" },
                    { "69", "SECOND-CLASS ENGINEER, STEAMSHIP", "OFFICIER MÉCANICIEN DE DEUXIÈME CLASSE, NAVIRE À VAPEUR" },
                    { "70", "SHIP PERSONNEL WITH DESIGNATED SECURITY DUTIES", "PERSONNEL DU NAVIRE AYANT DES RESPONSABILITÉS EN MATIÈRE DE SÛRETÉ" },
                    { "71", "SHIP PERSONNEL WITHOUT DESIGNATED SECURITY DUTIES", "PERSONNEL DU NAVIRE N'AYANT PAS DE RESPONSABILITÉS EN MATIÈRE DE SÛRETÉ" },
                    { "72", "SHIP SECURITY OFFICER", "AGENT DE SÛRETÉ DU NAVIRE" },
                    { "73", "SPECIALIZED PASSENGER SAFETY MANAGEMENT (RO-RO VESSELS)", "GESTION SPÉCIALISÉE DE LA SÉCURITÉ DES PASSAGERS (BÂTIMENTS ROULIERS)" },
                    { "74", "STCW BASIC SAFETY", "SÉCURITÉ DE BASE STCW" },
                    { "75", "SURVIVAL CRAFT AND RESCUE BOATS OTHER THAN FAST RESCUE BOATS", "EXPLOITATION DES BATEAUX DE SAUVETAGE ET CANOTS DE SECOURS, AUTRES QUE DES CANOTS DE SECOURS RAPIDES" },
                    { "78", "THIRD-CLASS ENGINEER, MOTOR SHIP (3M)", "OFFICIER MÉCANICIEN DE TROISIÈME CLASSE, NAVIRE À MOTEUR (3M)" },
                    { "79", "THIRD-CLASS ENGINEER, MOTOR SHIP (3MN)", "OFFICIER MÉCANICIEN DE TROISIÈME CLASSE, NAVIRE À MOTEUR (3MN)" },
                    { "80", "THIRD-CLASS ENGINEER, MOTOR SHIP FOURTH-CLASS ENGINEER, STEAMSHIP", "OFFICIER MÉCANICIEN DE TROISIÈME CLASSE, NAVIRE À MOTEUR OFFICIER MÉCANICIEN DE QUATRIÈME CLASSE, NAVIRE À VAPEUR" },
                    { "81", "THIRD-CLASS ENGINEER, STEAMSHIP (3S)", "OFFICIER MÉCANICIEN DE TROISIÈME CLASSE, NAVIRE À VAPEUR (3S)" },
                    { "82", "THIRD-CLASS ENGINEER, STEAMSHIP (3SN)", "OFFICIER MÉCANICIEN DE TROISIÈME CLASSE, NAVIRE À VAPEUR (3SN)" },
                    { "83", "WATCHKEEPING MATE", "OFFICIER DE PONT DE QUART" },
                    { "84", "WATCHKEEPING MATE, NEAR COASTAL", "OFFICIER DE PONT DE QUART, À PROXIMITÉ DU LITTORAL" },
                };

            Random random = new Random();

            for (int n = 0; n < 100; n++)
            {
                DashboardRow row = new DashboardRow();
                row.ServiceRequestNumber = 1000 + n;
                row.FullName = randomNames[n];

                int random_6_diget_number = random.Next(100001, 999999);
                row.CDN = random_6_diget_number + "K";

                int certificateIndex = random.Next(1, 66);
                string certificateType = certificates[certificateIndex, 1];  // 1 is for getting English value
                row.RequestType = certificateType;


                int staffIndex = random.Next(1, 6);
                row.AssignedTo = randomStaffs[staffIndex];

                int randomDays = random.Next(1, 22);
                row.ServiceStandard = randomDays;

                int processingPhaseIndex = random.Next(0, 4);
                row.ProcessingPhase = processingPhases[processingPhaseIndex];

                requestList.Add(row);

            }

            return requestList;

        }

        public int FillMockDataForGrids(ref List<DashboardRow> newRequests,
                                        ref List<DashboardRow> requestsInProgress,
                                        ref List<DashboardRow> requestsOnHold,
                                        ref List<DashboardRow> requestsCompleted,
                                        ref List<DashboardRow> requestsNotSubmitted)
        {
            var allRequests = this.FillMockRows();

            newRequests = allRequests.Where(x => x.ProcessingPhase == Constants.New).ToList();
            requestsInProgress = allRequests.Where(x => x.ProcessingPhase == Constants.InProgress).ToList();
            requestsOnHold = allRequests.Where(x => x.ProcessingPhase == Constants.OnHold).ToList();
            requestsCompleted = allRequests.Where(x => x.ProcessingPhase == Constants.Completed).ToList();
            requestsNotSubmitted = allRequests.Where(x => x.ProcessingPhase == Constants.NotSubmitted).ToList();

            int numberOfTotalRequests = allRequests.Count; 
            return numberOfTotalRequests;
        }

        public RequestDetailsPageModel CreateMockRequestDetailsData()
        {
            RequestDetailsPageModel requestDetailsPageModel = new RequestDetailsPageModel();

            SeafarerDetailsModel seafarerDetails = new SeafarerDetailsModel();
            seafarerDetails.FirstName = "Colin";
            seafarerDetails.LastName = "Gunanayagam";
            seafarerDetails.CDN = "456257P";
            seafarerDetails.DateOfBirth = new DateTime(1979, 07, 28);

            seafarerDetails.FitnessStatus = "Fit";
            seafarerDetails.Limitations.Add("Work day shifts only");
            seafarerDetails.Limitations.Add("Near sighted");
            seafarerDetails.DateOfExpiry = new DateTime(2026, 02, 23);

            seafarerDetails.CurrentCertificates.Add(new CertificateInfo("Master Near Coastal", new DateTime(2026, 02, 23)));
            seafarerDetails.CurrentCertificates.Add(new CertificateInfo("Passenger Safety Management", new DateTime(2022, 06, 11)));
            seafarerDetails.CurrentCertificates.Add(new CertificateInfo("Master Mariner", new DateTime(2024, 12, 15)));

            requestDetailsPageModel.SeafarerDetails = seafarerDetails;
            return requestDetailsPageModel;
        }
    }
}
