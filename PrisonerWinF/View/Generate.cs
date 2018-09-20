using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrisonerWinF.Model;

namespace PrisonerWinF.View
{
    static class Generate
    {
        static Random rand = new Random((int)DateTime.Now.Ticks);

        static string[] firstNames = { "Abner", "Adam", "Adelbert",
                                  "Alexander ", "Andrew ", "Benjamin",
                                  "Bruce", "Christian", "Christopher",
                                  "David", "Dennis","Edgar",
                                  "Felix","Garrett","Herman",
                                  "Homer","Jeffrey","Kevin",
                                  "Adolfo","Alvin","Alex",
                                  "Arthur","Barry","Brian",
                                  "Ben","Boris","Brendon",
                                  "Billy","Chester","Carlos",
                                  "Clayton","Curt","Cedric",
                                  "Damian","Dominique","Dorian",
                                  "Drew","Dillon","Daron",
                                  "Eddy","Erick","Erasmo",
                                  "Emilio","Fletcher","Federico",
                                  "Freddy","Fernando","Gerald",
                                  "Gabriel","Gordon","Giuseppe",
                                  "Henry","Hugo","Jack",
                                  "Jimmy","Mason","Maxwell"
        };

        static string[] lastNames = { "Wayne", "Smith","Guerrero",
                                 "Ernesto","Xiao","Hungary",
                                 "Johson","Williams","Miller",
                                 "Davis","Clark","Walker",
                                 "Allen","Scott","Evans",
                                 "Baker","Harris","Lee",
                                 "Kline","Russell","George",
                                 "Bradley","Woodward","Bailey",
                                 "Underwood","Hale","Marsh",
                                 "Mcclain","Lutz","Stanton",
                                 "Kelly","Green","Schroeder",
                                 "Abbott","Snyder","Mathis",
                                 "Walls","Patrick","Hodges",
                                 "Gentry","Ritter","Mahoney"
        };

        static string[] nickNames = { "Scarface", "Blackbeard","Jack the Ripper",
                                 "The Zodiac Killer","The Killer Clown","Billy The Kid",
                                 "Dasher","Pistol Pete","Ice Cube",
                                 "Ice Cube", "Snoop Dogg", "Ice-T",
                                 "The Geto Boys", "Hawk", "Shy Girl",
                                 "Kraken","Mad Dog","Cobra",
                                 "Diablo","Doom","Zero",
                                 "Bullet-Proof","Fire-Bred","Iron-Cut",
                                 "Manticore","Mothman","Furor",
                                 "Fury","Ire","Mania",
                                 "Manic","Atilla","Terminator",
                                 "Napoleon","Hannibal","Leonidas",
                                 "Agrippa","Suleiman","Billy the Butcher",
                                 "X-Skull","Bleed","Skeleton",
                                 "Footslam","Tooth",
                                 "Kneecap","Blood","Finisher",
                                 "Destroyer","Hitter","Exterminator"
        };

        static string[] citizenships = {"Afghanistan", "Albania", "Argentina",
                                     "Armenia", "Australia", "Bulgaria	",
                                     "Canada", "China", "Colombia",
                                     "Congo","Cuba","Egypt",
                                     "Germany","India","Israel",
                                     "Italy","Japan","Kenya",
                                     "Latvia","Luxembourg","Maldives",
                                     "Malta","Mexico","Monaco",
                                     "Nigeria","Panama","Poland",
                                     "Portugal","Romania","Serbia",
                                     "Slovakia","Singapore","Spain",
                                     "Thailand","Tunisia","Uganda",
                                     "Uruguay","Yemen","Zambia",
                                     "Zimbabwe","Uzbekistan","United States",
                                     "Ukraine","Turkmenistan","Tunisia",
                                     "Togo","Timor-Leste","Switzerland" };

        static string[] lasthings = { "Aggravated assault","Aiding and Abetting", "Arson",
                                    "Battery", "Bribery","Burglary",
                                    "Child Abuse", "Child Pornography","Computer Crime",
                                    "Credit Card Fraud", "Disorderly Conduct","Disturbing the Peace",
                                    "Drug Cultivation", "Drug Possession","Drunk Driving",
                                    "Embezzlement","Extortion","Fraud",
                                    "Forgery","Harassment","Identity Theft",
                                    "Kidnapping","Money Laundering","Murder",
                                    "Perjury","Public Intoxication","Rape" };

        static string[] specialSigns = { "Thick eyebrows", "Long hair", "Pigtail",
                                "A scar on his face","Tattoos on the legs","Tattoos on the arm",
                                "Tattoos on the back","A birthmark on the face","Birthmark on the arm",
                                "Aquiline nose","Different eyes color" };

        static string[] knowledgeOfLanguages = {"English",  "Spanish, English", "Arabic, English",
                                             "German","Arabic, German","German, English",
                                             "Polish, German","German, Russian","Russian, English",
                                             "Ukraine, Polish","Polish, English","Polish, Russian",
                                             "English, Ukraine","Ukraine, Russian, English","German, Russian",
                                             "France, Ukraine","English, France","Spanish",
                                             "France, Russian","English, Spanish","France, Russian, Ukraine" };

        static Photo[] photos =
        {
            new Photo
            {
                firstPhoto = @"..\..\..\prisonersPhoto\1\1-Ф.jpg",
                secondPhoto = @"..\..\..\prisonersPhoto\1\1-П.jpg"
            },
            new Photo
            {
                firstPhoto = @"..\..\..\prisonersPhoto\1\2-Ф.jpg",
                secondPhoto = @"..\..\..\prisonersPhoto\1\2-П.jpg"
            },
            new Photo
            {
                firstPhoto = @"..\..\..\prisonersPhoto\1\3-Ф.jpg",
                secondPhoto = @"..\..\..\prisonersPhoto\1\3-П.jpg"
            },
            new Photo
            {
                firstPhoto = @"..\..\..\prisonersPhoto\1\4-Ф.jpg",
                secondPhoto = @"..\..\..\prisonersPhoto\1\4-П.jpg"
            },
            new Photo
            {
                firstPhoto = @"..\..\..\prisonersPhoto\1\5-Ф.jpg",
                secondPhoto = @"..\..\..\prisonersPhoto\1\5-П.jpg"
            },
            new Photo
            {
                firstPhoto = @"..\..\..\prisonersPhoto\1\6-Ф.jpg",
                secondPhoto = @"..\..\..\prisonersPhoto\1\6-П.jpg"
            },
            new Photo
            {
                firstPhoto = @"..\..\..\prisonersPhoto\1\7-Ф.jpg",
                secondPhoto = @"..\..\..\prisonersPhoto\1\7-П.jpg"
            },
            new Photo
            {
                firstPhoto = @"..\..\..\prisonersPhoto\1\8-Ф.jpg",
                secondPhoto = @"..\..\..\prisonersPhoto\1\8-П.jpg"
            },
            new Photo
            {
                firstPhoto = @"..\..\..\prisonersPhoto\1\9-Ф.jpg",
                secondPhoto = @"..\..\..\prisonersPhoto\1\9-П.jpg"
            },
            new Photo
            {
                firstPhoto = @"..\..\..\prisonersPhoto\1\10-Ф.jpg",
                secondPhoto = @"..\..\..\prisonersPhoto\1\10-П.jpg"
            },
            new Photo
            {
                firstPhoto = @"..\..\..\prisonersPhoto\1\11-Ф.jpg",
                secondPhoto = @"..\..\..\prisonersPhoto\1\11-П.jpg"
            },
            new Photo
            {
                firstPhoto = @"..\..\..\prisonersPhoto\1\12-Ф.jpg",
                secondPhoto = @"..\..\..\prisonersPhoto\1\12-П.jpg"
            },
            new Photo
            {
                firstPhoto = @"..\..\..\prisonersPhoto\1\13-Ф.jpg",
                secondPhoto = @"..\..\..\prisonersPhoto\1\13-П.jpg"
            },
            new Photo
            {
                firstPhoto = @"..\..\..\prisonersPhoto\1\14-Ф.jpg",
                secondPhoto = @"..\..\..\prisonersPhoto\1\14-П.jpg"
            },
            new Photo
            {
                firstPhoto = @"..\..\..\prisonersPhoto\1\15-Ф.jpg",
                secondPhoto = @"..\..\..\prisonersPhoto\1\15-П.jpg"
            },
            new Photo
            {
                firstPhoto = @"..\..\..\prisonersPhoto\1\16-Ф.jpg",
                secondPhoto = @"..\..\..\prisonersPhoto\1\16-П.jpg"
            },
            new Photo
            {
                firstPhoto = @"..\..\..\prisonersPhoto\1\17-Ф.jpg",
                secondPhoto = @"..\..\..\prisonersPhoto\1\17-П.jpg"
            },
            new Photo
            {
                firstPhoto = @"..\..\..\prisonersPhoto\1\18-Ф.jpg",
                secondPhoto = @"..\..\..\prisonersPhoto\1\18-П.jpg"
            }
        };

        static public Photo GetPhotos()
        {
            return photos[rand.Next(0, photos.Length - 1)];
        }

        static public string GetfirstNameTextBox()
        {
            return firstNames[rand.Next(0, firstNames.Length - 1)];
        }

        static public string GetlastNameTextBox()
        {
            return lastNames[rand.Next(0, lastNames.Length - 1)];
        }

        static public string GetnickNameTextBox()
        {
            return nickNames[rand.Next(0, nickNames.Length - 1)];
        }

        static public string GetcitizenshipTextBox()
        {
            return citizenships[rand.Next(0, citizenships.Length - 1)];
        }

        static public string GetlastPlaceOfResidenceTextBox()
        {
            return citizenships[rand.Next(0, citizenships.Length - 1)];
        }

        static public string GetlastThingTextBox()
        {
            return lasthings[rand.Next(0, lasthings.Length - 1)];
        }

        static public string GetspecialSignsTextBox()
        {
            return specialSigns[rand.Next(0, specialSigns.Length - 1)];
        }

        static public string GetknowledgeOfLanguagesTextBox()
        {
            return knowledgeOfLanguages[rand.Next(0, knowledgeOfLanguages.Length - 1)];
        }
    }
}
