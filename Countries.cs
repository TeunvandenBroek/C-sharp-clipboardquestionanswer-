using System.Collections.Generic;
using System.Linq;

namespace it
{
    internal static partial class Countries
    {
        public static class Offset
        {
            public const byte Hours = 4;
            public const byte HalfHours = 2;
            public const byte QuarterHours = 1;
            public const byte ThreeQuarters = 3 * QuarterHours;
        }
        public static Dictionary<UtcOffset, string[]> CountriesByUtcOffset { get; } = new Dictionary<UtcOffset, string[]>
        {
            [UtcOffset.UtcMinusTwelve] = new[]
            {
                "baker","howland"
            },
            [UtcOffset.UtcMinusEleven] = new[]
            {
                "amerikaans-Samoa","midway-eilanden","niue"
            },
            [UtcOffset.UtcMinusTen] = new[]
            {
                "cookeilanden","Jonhnston-atol","Hawai"
            },
            [UtcOffset.UtcMinusNine] = new[]
            {
                "alaska","gambiereilanden"
            },
            [UtcOffset.UtcMinusEight] = new[]
            {
                "brits-columbia","yukon","neder-californie", "californie","nevada","oregon","washington","pitcairneilanden"
            },
            [UtcOffset.UtcMinusSeven] = new[]
            {
                "alberta", "northwest territories", "nunavut","chihuahua", "sinaloa", "sonora", "nayarit", "zuid-neder-californië",
                "colorado", "idaho", "montana", "nebraska", "new mexico", "north dakota", "south dakota", "utah", "wyoming"
            },
            [UtcOffset.UtcMinusSix] = new[]
            {
                "belize","costa rica","el salvador","guatemala","honduras","nicaragua","alabama", "arkansas", "illinois", "iowa", "kansas", "louisiana", "minnesota", "mississippi", "missouri", "oklahoma", "texas", "wisconsin"
            },
            [UtcOffset.UtcMinusFive] = new[]
            {
                "colombia","cuba","ecuador","haiti","jamaica","kaaimaneilanden","panama","peru","turks- en caicoseilanden",
                "connecticut","delaware","district of columbia","florida","georgia","kentucky","maine","maryland","massachusetts","michigan","new hampshire","new jersey","new york","north carolina","ohio","pennsylvania","rhode island","south carolina","tennessee","vermont","virginia","westvirginia"
            },
            [UtcOffset.UtcMinusFour] = new[]
            {
                "amerikaanse maagdeneilanden","anguilla","antigua en barbuda","aruba","barbados","bolivia","britse maagdeneilanden","curacao",
                "dominica","dominicaanse republiek","bermuda","falklandeilanden","grenada","guadeloupe","guyana","martinique","montserrat","caribische eilanden",
                "paraguay","puerto rico","saint kitts en nevis","saint vincent en de grenadines","sint maarten","trinidad en tobago","venezuela"
            },
            [UtcOffset.UtcMinusThreepoinfive] = new[]
            {
                "newfoundland",
            },
            [UtcOffset.UtcMinusThree] = new[]
            {
                "argentinie","brazilie","chili","frans-guyana","saint-pierre en miquelon","suriname","uruguay"
            },
            [UtcOffset.UtcMinusTwo] = new[]
            {
                 "fernando de noronha"
            },
            [UtcOffset.UtcMinusOne] = new[]
            {
                "kaapverdie","groenland","azoren"
            },
            [UtcOffset.UtcZero] = new[]
            {
                "burkina faso","faeroer","gambia","ghana","guinee","guinee-bissau","ijsland","ierland",
                "ivoorkust","liberia","mali","mauritanie","marokko","portugalsint-helena","senegal","sierra leone",
                "canarische eilanden","togo","verenigd koninkrijk"
            },
            [UtcOffset.UtcPlusOne] = new[]
            {
                "albanie", "algerije", "andorra", "angola", "belgie", "benin",
                "bosnie en herzegovina", "centraal-afrikaanse republiek",
                "congo-brazzaville", "congo-kinshasa", "denemarken", "duitsland",
                "equatoriaal-guinea", "frankrijk", "gabon", "gibraltar", "hongarije",
                "italie", "kameroen", "kosovo", "kroatie", "liechtenstein", "luxemburg",
                "malta", "monaco", "montenegro", "namibie", "nederland", "niger", "nigeria",
                "noord-macedonie", "noorwegen", "oostenrijk", "polen", "sao tome en principe",
                "san marino", "servie", "slowakije", "slovenie", "spanje", "spitsbergen en jan mayen",
                "tsjaad", "tsjechie",  "tunesie", "vaticaanstad", "zweden", "zwitserland"
            },
            [UtcOffset.UtcPlusTwo] = new[]
            {
                "aland","botswana","bulgarije","burundi","congo","cyprus","egypte"
                ,"estland","finland","griekenland","israel","letland","libanon","lesotho","litouwen","libie","malawi","moldavie"
                ,"mozambique","oekraine","palestina","roemenie","rusland","rwanda","soedan","swaziland","syrie","zambia","zimbabwe","zuid-afrika","zuid-soedan"
            },
            [UtcOffset.UtcPlusThree] = new[]
            {
                "bahrein","comoren","djibouti","eritrea","ethiopie",
                "irak","jemen","jordanie","kenia","koeweit","madagaskar","mayotte","oeganda","qatar",
                "saoedi-arabie","tanzania","turkije","wit-rusland"
            },
            [UtcOffset.UtcPlusThreepoinfive] = new[]
            {
                "iran"
            },
            [UtcOffset.UtcPlusFour] = new[]
            {
                 "armenie","georgie","mauritius"
                 ,"oman","reunion","seychellen","verenigdearabischeemiraten"
            },
            [UtcOffset.UtcPlusFourpointfive] = new[]
            {
                "afganistan"
            },
            [UtcOffset.UtcPlusFive] = new[]
            {
                 "azerbeidzjan", "kazachstan", "maldiven"
                , "oezbekistan", "pakistan",  "jekaterinenburg", "perm", "tadzjikistan", "turkmenistan"
            },
            [UtcOffset.UtcPlusFivepointfive] = new[]
            {
                "india", "sri lanka"
            },
            [UtcOffset.UtcPlusFivepointThreeQuarters] = new[]
            {
                "nepal" //three quarter isnt correct i think
            },
            [UtcOffset.UtcPlusSix] = new[]
            {
                 "bangladesh","bhutan", "kirgizie"
            },
            [UtcOffset.UtcPlusSeven] = new[]
            {
                 "cambodja","christmaseiland","indonesie","laos","thailand","vietnam"
            },
            [UtcOffset.UtcPlusEight] = new[]
            {
                 "australie","brunei","china","filipijnen","hongkong","macau","maleisie","mongolie","singapore","taiwan"
            },
            [UtcOffset.UtcPlusNine] = new[]
            {
                 "japan","noord-korea","zuid-korea","oost-timor","palau"
            },
            [UtcOffset.UtcPlusTen] = new[]
            {
                 "guam","micronesie","noordelijke marianen","papoea-nieuw-guinea"
            },
            [UtcOffset.UtcPlusEleven] = new[]
            {
                 "nieuw-caledonie","salomonseilanden","vanuatu"
            },
            [UtcOffset.UtcPlusTwelve] = new[]
            {
                 "fijl","kiribati","marshalleilanden","nauru","nieuw-zeeland","tuvalu","wake-eiland","wallis en futuna"
            },
            [UtcOffset.UtcPlusThirteen] = new[]
            {
                 "samoa", "tokelau", "tonga"
            },
        };

        public static Dictionary<string, UtcOffset> UtcOffsetByCountry { get; } = CountriesByUtcOffset
            .SelectMany(x => x.Value.Select(c => { return (Offset: x.Key, Country: c); }))
            .ToDictionary(x => x.Country, x => x.Offset);

        //UTC-12
        public static IEnumerable<string> CountriesUTCmin12 => CountriesByUtcOffset[UtcOffset.UtcMinusTwelve];
        //UTC-11
        public static IEnumerable<string> CountriesUTCmin11 => CountriesByUtcOffset[UtcOffset.UtcMinusEleven];
        //UTC-10
        public static IEnumerable<string> CountriesUTCmin10 => CountriesByUtcOffset[UtcOffset.UtcMinusTen];
        //UTC-9
        public static IEnumerable<string> CountriesUTCmin9 => CountriesByUtcOffset[UtcOffset.UtcMinusNine];
        //UTC-8
        public static IEnumerable<string> CountriesUTCmin8 => CountriesByUtcOffset[UtcOffset.UtcMinusEight];
        //UTC-7
        public static IEnumerable<string> CountriesUTCmin7 => CountriesByUtcOffset[UtcOffset.UtcMinusSeven];
        //UTC-6
        public static IEnumerable<string> CountriesUTCmin6 => CountriesByUtcOffset[UtcOffset.UtcMinusSix];
        //UTC-5
        public static IEnumerable<string> CountriesUTCmin5 => CountriesByUtcOffset[UtcOffset.UtcMinusFive];
        //UTC-4
        public static IEnumerable<string> CountriesUTCmin4 => CountriesByUtcOffset[UtcOffset.UtcMinusFour];
        //UTC-3.5
        public static IEnumerable<string> CountriesUTCmin3point5 => CountriesByUtcOffset[UtcOffset.UtcMinusThreepoinfive];
        //UTC-3
        public static IEnumerable<string> CountriesUTCmin3 => CountriesByUtcOffset[UtcOffset.UtcMinusThree];
        //UTC-2
        public static IEnumerable<string> CountriesUTCmin2 => CountriesByUtcOffset[UtcOffset.UtcMinusTwo];
        //UTC-1
        public static IEnumerable<string> CountriesUTCmin1 => CountriesByUtcOffset[UtcOffset.UtcMinusOne];
        //UTC+0
        public static IEnumerable<string> CountriesUTC0 => CountriesByUtcOffset[UtcOffset.UtcZero];
        //UTC+1
        public static IEnumerable<string> CountriesUTC1 => CountriesByUtcOffset[UtcOffset.UtcPlusOne];

        //lijst met landen met utc2
        public static IEnumerable<string> CountriesUTC2 => CountriesByUtcOffset[UtcOffset.UtcPlusTwo];

        //UTC3
        public static IEnumerable<string> CountriesUTC3 => CountriesByUtcOffset[UtcOffset.UtcPlusThree];
        //UTC3.5
        public static IEnumerable<string> CountriesUTC3point5 => CountriesByUtcOffset[UtcOffset.UtcPlusThreepoinfive];
        //UTC4
        public static IEnumerable<string> CountriesUTC4 => CountriesByUtcOffset[UtcOffset.UtcPlusFour];

        //UTC 4.5
        public static IEnumerable<string> CountriesUTC4point5 => CountriesByUtcOffset[UtcOffset.UtcPlusFourpointfive];

        //UTC5
        public static IEnumerable<string> CountriesUTC5 => CountriesByUtcOffset[UtcOffset.UtcPlusFive];
        //5.5
        public static IEnumerable<string> CountriesUTC5point5 => CountriesByUtcOffset[UtcOffset.UtcPlusFivepointfive];
        //utc5,75
        public static IEnumerable<string> CountriesUTC5punt75 => CountriesByUtcOffset[UtcOffset.UtcPlusFivepointThreeQuarters];
        //Utc6
        public static IEnumerable<string> CountriesUTC6 => CountriesByUtcOffset[UtcOffset.UtcPlusSix];
        //utc6,5
        public static IEnumerable<string> CountriesUTC6punt5 => CountriesByUtcOffset[UtcOffset.UtcPlusSixpointfive];
        //Utc7
        public static IEnumerable<string> CountriesUTC7 => CountriesByUtcOffset[UtcOffset.UtcPlusSeven];
        //Utc8
        public static IEnumerable<string> CountriesUTC8 => CountriesByUtcOffset[UtcOffset.UtcPlusEight];
        //Utc9
        public static IEnumerable<string> CountriesUTC9 => CountriesByUtcOffset[UtcOffset.UtcPlusNine];
        //Utc10
        public static IEnumerable<string> CountriesUTC10 => CountriesByUtcOffset[UtcOffset.UtcPlusTen];
        //Utc11
        public static IEnumerable<string> CountriesUTC11 => CountriesByUtcOffset[UtcOffset.UtcPlusEleven];
        //Utc12
        public static IEnumerable<string> CountriesUTC12 => CountriesByUtcOffset[UtcOffset.UtcPlusTwelve];
        //Utc13
        public static IEnumerable<string> CountriesUTC13 => CountriesByUtcOffset[UtcOffset.UtcPlusThirteen];
    }
}
