using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace KeyNekretnine.Migrations
{
    public partial class AddingPhoneNumbersAgencyLanguagesAndLanguages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "agencies",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "facebook_url",
                table: "agencies",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "instagram_url",
                table: "agencies",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "latitude",
                table: "agencies",
                type: "double precision",
                maxLength: 91,
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "linkedln",
                table: "agencies",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "location",
                table: "agencies",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "longitude",
                table: "agencies",
                type: "double precision",
                maxLength: 181,
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "twitter_url",
                table: "agencies",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "website_url",
                table: "agencies",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "work_end_time",
                table: "agencies",
                type: "time without time zone",
                maxLength: 50,
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<TimeOnly>(
                name: "work_start_time",
                table: "agencies",
                type: "time without time zone",
                maxLength: 50,
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.CreateTable(
                name: "languages",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_languages", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "phone_numbers",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    label = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    phone = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_phone_numbers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "agency_languages",
                columns: table => new
                {
                    agency_id = table.Column<int>(type: "integer", nullable: false),
                    language_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_agency_languages", x => new { x.agency_id, x.language_id });
                    table.ForeignKey(
                        name: "fk_agency_languages_agencies_agency_id",
                        column: x => x.agency_id,
                        principalTable: "agencies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_agency_languages_languages_language_id",
                        column: x => x.language_id,
                        principalTable: "languages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "asp_net_roles",
                keyColumn: "id",
                keyValue: "548e52a6-485a-49a5-b204-8994eaa79a12",
                column: "concurrency_stamp",
                value: "623201e9-e3e9-437d-9b8d-69fde79e38cb");

            migrationBuilder.UpdateData(
                table: "asp_net_roles",
                keyColumn: "id",
                keyValue: "62558fd6-61f6-42fe-8cb7-8bc5fea7fb93",
                column: "concurrency_stamp",
                value: "cb91afdf-25ec-4f3c-af5b-cc175333093b");

            migrationBuilder.UpdateData(
                table: "asp_net_roles",
                keyColumn: "id",
                keyValue: "f78fff4a-06dc-4b5d-864c-d70cd9ced860",
                column: "concurrency_stamp",
                value: "bb966937-da47-4c5c-abc4-dd02066a5699");

            migrationBuilder.InsertData(
                table: "phone_numbers",
                columns: new[] { "id", "code", "label", "phone" },
                values: new object[,]
                {
                    { 1, "AD", "Andorra", "376" },
                    { 2, "AE", "United Arab Emirates", "971" },
                    { 3, "AF", "Afghanistan", "93" },
                    { 4, "AG", "Antigua and Barbuda", "1-268" },
                    { 5, "AI", "Anguilla", "1-264" },
                    { 6, "AL", "Albania", "355" },
                    { 7, "AM", "Armenia", "374" },
                    { 8, "AO", "Angola", "244" },
                    { 9, "AQ", "Antarctica", "672" },
                    { 10, "AR", "Argentina", "54" },
                    { 11, "AS", "American Samoa", "1-684" },
                    { 12, "AT", "Austria", "43" },
                    { 13, "AU", "Australia", "61" },
                    { 14, "AW", "Aruba", "297" },
                    { 15, "AX", "Alland Islands", "358" },
                    { 16, "AZ", "Azerbaijan", "994" },
                    { 17, "BA", "Bosnia and Herzegovina", "387" },
                    { 18, "BB", "Barbados", "1-246" },
                    { 19, "BD", "Bangladesh", "880" },
                    { 20, "BE", "Belgium", "32" },
                    { 21, "BF", "Burkina Faso", "226" },
                    { 22, "BG", "Bulgaria", "359" },
                    { 23, "BH", "Bahrain", "973" },
                    { 24, "BI", "Burundi", "257" },
                    { 25, "BJ", "Benin", "229" },
                    { 26, "BL", "Saint Barthelemy", "590" },
                    { 27, "BM", "Bermuda", "1-441" },
                    { 28, "BN", "Brunei Darussalam", "673" },
                    { 29, "BO", "Bolivia", "591" },
                    { 30, "BR", "Brazil", "55" },
                    { 31, "BS", "Bahamas", "1-242" },
                    { 32, "BT", "Bhutan", "975" },
                    { 33, "BV", "Bouvet Island", "47" },
                    { 34, "BW", "Botswana", "267" },
                    { 35, "BY", "Belarus", "375" },
                    { 36, "BZ", "Belize", "501" },
                    { 37, "CA", "Canada", "1" },
                    { 38, "CC", "Cocos (Keeling) Islands", "61" },
                    { 39, "CD", "Congo, Democratic Republic of the", "243" },
                    { 40, "CF", "Central African Republic", "236" },
                    { 41, "CG", "Congo, Republic of the", "242" },
                    { 42, "CH", "Switzerland", "41" },
                    { 43, "CI", "Cote d'Ivoire", "225" },
                    { 44, "CK", "Cook Islands", "682" },
                    { 45, "CL", "Chile", "56" },
                    { 46, "CM", "Cameroon", "237" },
                    { 47, "CN", "China", "86" },
                    { 48, "CO", "Colombia", "57" },
                    { 49, "CR", "Costa Rica", "506" },
                    { 50, "CU", "Cuba", "53" },
                    { 51, "CV", "Cape Verde", "238" },
                    { 52, "CW", "Curacao", "599" },
                    { 53, "CX", "Christmas Island", "61" },
                    { 54, "CY", "Cyprus", "357" },
                    { 55, "CZ", "Czech Republic", "420" },
                    { 56, "DE", "Germany", "49" },
                    { 57, "DJ", "Djibouti", "253" },
                    { 58, "DK", "Denmark", "45" },
                    { 59, "DM", "Dominica", "1-767" },
                    { 60, "DO", "Dominican Republic", "1-809" },
                    { 61, "DZ", "Algeria", "213" },
                    { 62, "EC", "Ecuador", "593" },
                    { 63, "EE", "Estonia", "372" },
                    { 64, "EG", "Egypt", "20" },
                    { 65, "EH", "Western Sahara", "212" },
                    { 66, "ER", "Eritrea", "291" },
                    { 67, "ES", "Spain", "34" },
                    { 68, "ET", "Ethiopia", "251" },
                    { 69, "FI", "Finland", "358" },
                    { 70, "FJ", "Fiji", "679" },
                    { 71, "FK", "Falkland Islands (Malvinas)", "500" },
                    { 72, "FM", "Micronesia, Federated States of", "691" },
                    { 73, "FO", "Faroe Islands", "298" },
                    { 74, "FR", "France", "33" },
                    { 75, "GA", "Gabon", "241" },
                    { 76, "GB", "United Kingdom", "44" },
                    { 77, "GD", "Grenada", "1-473" },
                    { 78, "GE", "Georgia", "995" },
                    { 79, "GF", "French Guiana", "594" },
                    { 80, "GG", "Guernsey", "44" },
                    { 81, "GH", "Ghana", "233" },
                    { 82, "GI", "Gibraltar", "350" },
                    { 83, "GL", "Greenland", "299" },
                    { 84, "GM", "Gambia", "220" },
                    { 85, "GN", "Guinea", "224" },
                    { 86, "GP", "Guadeloupe", "590" },
                    { 87, "GQ", "Equatorial Guinea", "240" },
                    { 88, "GR", "Greece", "30" },
                    { 89, "GS", "South Georgia and the South Sandwich Islands", "500" },
                    { 90, "GT", "Guatemala", "502" },
                    { 91, "GU", "Guam", "1-671" },
                    { 92, "GW", "Guinea-Bissau", "245" },
                    { 93, "GY", "Guyana", "592" },
                    { 94, "HK", "Hong Kong", "852" },
                    { 95, "HM", "Heard Island and McDonald Islands", "672" },
                    { 96, "HN", "Honduras", "504" },
                    { 97, "HR", "Croatia", "385" },
                    { 98, "HT", "Haiti", "509" },
                    { 99, "HU", "Hungary", "36" },
                    { 100, "ID", "Indonesia", "62" },
                    { 101, "IE", "Ireland", "353" },
                    { 102, "IL", "Israel", "972" },
                    { 103, "IM", "Isle of Man", "44" },
                    { 104, "IN", "India", "91" },
                    { 105, "IO", "British Indian Ocean Territory", "246" },
                    { 106, "IQ", "Iraq", "964" },
                    { 107, "IR", "Iran, Islamic Republic of", "98" },
                    { 108, "IS", "Iceland", "354" },
                    { 109, "IT", "Italy", "39" },
                    { 110, "JE", "Jersey", "44" },
                    { 111, "JM", "Jamaica", "1-876" },
                    { 112, "JO", "Jordan", "962" },
                    { 113, "JP", "Japan", "81" },
                    { 114, "KE", "Kenya", "254" },
                    { 115, "KG", "Kyrgyzstan", "996" },
                    { 116, "KH", "Cambodia", "855" },
                    { 117, "KI", "Kiribati", "686" },
                    { 118, "KM", "Comoros", "269" },
                    { 119, "KN", "Saint Kitts and Nevis", "1-869" },
                    { 120, "KP", "Korea, Democratic People's Republic of", "850" },
                    { 121, "KR", "Korea, Republic of", "82" },
                    { 122, "KW", "Kuwait", "965" },
                    { 123, "KY", "Cayman Islands", "1-345" },
                    { 124, "KZ", "Kazakhstan", "7" },
                    { 125, "LA", "Lao People's Democratic Republic", "856" },
                    { 126, "LB", "Lebanon", "961" },
                    { 127, "LC", "Saint Lucia", "1-758" },
                    { 128, "LI", "Liechtenstein", "423" },
                    { 129, "LK", "Sri Lanka", "94" },
                    { 130, "LR", "Liberia", "231" },
                    { 131, "LS", "Lesotho", "266" },
                    { 132, "LT", "Lithuania", "370" },
                    { 133, "LU", "Luxembourg", "352" },
                    { 134, "LV", "Latvia", "371" },
                    { 135, "LY", "Libya", "218" },
                    { 136, "MA", "Morocco", "212" },
                    { 137, "MC", "Monaco", "377" },
                    { 138, "MD", "Moldova, Republic of", "373" },
                    { 139, "ME", "Montenegro", "382" },
                    { 140, "MF", "Saint Martin (French part)", "590" },
                    { 141, "MG", "Madagascar", "261" },
                    { 142, "MH", "Marshall Islands", "692" },
                    { 143, "MK", "Macedonia, the Former Yugoslav Republic of", "389" },
                    { 144, "ML", "Mali", "223" },
                    { 145, "MM", "Myanmar", "95" },
                    { 146, "MN", "Mongolia", "976" },
                    { 147, "MO", "Macao", "853" },
                    { 148, "MP", "Northern Mariana Islands", "1-670" },
                    { 149, "MQ", "Martinique", "596" },
                    { 150, "MR", "Mauritania", "222" },
                    { 151, "MS", "Montserrat", "1-664" },
                    { 152, "MT", "Malta", "356" },
                    { 153, "MU", "Mauritius", "230" },
                    { 154, "MV", "Maldives", "960" },
                    { 155, "MW", "Malawi", "265" },
                    { 156, "MX", "Mexico", "52" },
                    { 157, "MY", "Malaysia", "60" },
                    { 158, "MZ", "Mozambique", "258" },
                    { 159, "NA", "Namibia", "264" },
                    { 160, "NC", "New Caledonia", "687" },
                    { 161, "NE", "Niger", "227" },
                    { 162, "NF", "Norfolk Island", "672" },
                    { 163, "NG", "Nigeria", "234" },
                    { 164, "NI", "Nicaragua", "505" },
                    { 165, "NL", "Netherlands", "31" },
                    { 166, "NO", "Norway", "47" },
                    { 167, "NP", "Nepal", "977" },
                    { 168, "NR", "Nauru", "674" },
                    { 169, "NU", "Niue", "683" },
                    { 170, "NZ", "New Zealand", "64" },
                    { 171, "OM", "Oman", "968" },
                    { 172, "PA", "Panama", "507" },
                    { 173, "PE", "Peru", "51" },
                    { 174, "PF", "French Polynesia", "689" },
                    { 175, "PG", "Papua New Guinea", "675" },
                    { 176, "PH", "Philippines", "63" },
                    { 177, "PK", "Pakistan", "92" },
                    { 178, "PL", "Poland", "48" },
                    { 179, "PM", "Saint Pierre and Miquelon", "508" },
                    { 180, "PN", "Pitcairn", "870" },
                    { 181, "PR", "Puerto Rico", "1" },
                    { 182, "PS", "Palestine, State of", "970" },
                    { 183, "PT", "Portugal", "351" },
                    { 184, "PW", "Palau", "680" },
                    { 185, "PY", "Paraguay", "595" },
                    { 186, "QA", "Qatar", "974" },
                    { 187, "RE", "Reunion", "262" },
                    { 188, "RO", "Romania", "40" },
                    { 189, "RS", "Serbia", "381" },
                    { 190, "RU", "Russian Federation", "7" },
                    { 191, "RW", "Rwanda", "250" },
                    { 192, "SA", "Saudi Arabia", "966" },
                    { 193, "SB", "Solomon Islands", "677" },
                    { 194, "SC", "Seychelles", "248" },
                    { 195, "SD", "Sudan", "249" },
                    { 196, "SE", "Sweden", "46" },
                    { 197, "SG", "Singapore", "65" },
                    { 198, "SH", "Saint Helena", "290" },
                    { 199, "SI", "Slovenia", "386" },
                    { 200, "SJ", "Svalbard and Jan Mayen", "47" },
                    { 201, "SK", "Slovakia", "421" },
                    { 202, "SL", "Sierra Leone", "232" },
                    { 203, "SM", "San Marino", "378" },
                    { 204, "SN", "Senegal", "221" },
                    { 205, "SO", "Somalia", "252" },
                    { 206, "SR", "Suriname", "597" },
                    { 207, "SS", "South Sudan", "211" },
                    { 208, "ST", "Sao Tome and Principe", "239" },
                    { 209, "SV", "El Salvador", "503" },
                    { 210, "SX", "Sint Maarten (Dutch part)", "1-721" },
                    { 211, "SY", "Syrian Arab Republic", "963" },
                    { 212, "SZ", "Swaziland", "268" },
                    { 213, "TC", "Turks and Caicos Islands", "1-649" },
                    { 214, "TD", "Chad", "235" },
                    { 215, "TF", "French Southern Territories", "262" },
                    { 216, "TG", "Togo", "228" },
                    { 217, "TH", "Thailand", "66" },
                    { 218, "TJ", "Tajikistan", "992" },
                    { 219, "TK", "Tokelau", "690" },
                    { 220, "TL", "Timor-Leste", "670" },
                    { 221, "TM", "Turkmenistan", "993" },
                    { 222, "TN", "Tunisia", "216" },
                    { 223, "TO", "Tonga", "676" },
                    { 224, "TR", "Turkey", "90" },
                    { 225, "TT", "Trinidad and Tobago", "1-868" },
                    { 226, "TV", "Tuvalu", "688" },
                    { 227, "TW", "Taiwan, Republic of China", "886" },
                    { 228, "TZ", "United Republic of Tanzania", "255" },
                    { 229, "UA", "Ukraine", "380" },
                    { 230, "UG", "Uganda", "256" },
                    { 231, "US", "United States", "1" },
                    { 232, "UY", "Uruguay", "598" },
                    { 233, "UZ", "Uzbekistan", "998" },
                    { 234, "VA", "Holy See (Vatican City State)", "379" },
                    { 235, "VC", "Saint Vincent and the Grenadines", "1-784" },
                    { 236, "VE", "Venezuela", "58" },
                    { 237, "VG", "British Virgin Islands", "1-284" },
                    { 238, "VI", "US Virgin Islands", "1-340" },
                    { 239, "VN", "Vietnam", "84" },
                    { 240, "VU", "Vanuatu", "678" },
                    { 241, "WF", "Wallis and Futuna", "681" },
                    { 242, "WS", "Samoa", "685" },
                    { 243, "XK", "Kosovo", "383" },
                    { 244, "YE", "Yemen", "967" },
                    { 245, "YT", "Mayotte", "262" },
                    { 246, "ZA", "South Africa", "27" },
                    { 247, "ZM", "Zambia", "260" },
                    { 248, "ZW", "Zimbabwe", "263" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_agency_languages_language_id",
                table: "agency_languages",
                column: "language_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "agency_languages");

            migrationBuilder.DropTable(
                name: "phone_numbers");

            migrationBuilder.DropTable(
                name: "languages");

            migrationBuilder.DropColumn(
                name: "email",
                table: "agencies");

            migrationBuilder.DropColumn(
                name: "facebook_url",
                table: "agencies");

            migrationBuilder.DropColumn(
                name: "instagram_url",
                table: "agencies");

            migrationBuilder.DropColumn(
                name: "latitude",
                table: "agencies");

            migrationBuilder.DropColumn(
                name: "linkedln",
                table: "agencies");

            migrationBuilder.DropColumn(
                name: "location",
                table: "agencies");

            migrationBuilder.DropColumn(
                name: "longitude",
                table: "agencies");

            migrationBuilder.DropColumn(
                name: "twitter_url",
                table: "agencies");

            migrationBuilder.DropColumn(
                name: "website_url",
                table: "agencies");

            migrationBuilder.DropColumn(
                name: "work_end_time",
                table: "agencies");

            migrationBuilder.DropColumn(
                name: "work_start_time",
                table: "agencies");

            migrationBuilder.UpdateData(
                table: "asp_net_roles",
                keyColumn: "id",
                keyValue: "548e52a6-485a-49a5-b204-8994eaa79a12",
                column: "concurrency_stamp",
                value: "9b8d4b68-1dfc-4d66-ade2-af54fc0f519a");

            migrationBuilder.UpdateData(
                table: "asp_net_roles",
                keyColumn: "id",
                keyValue: "62558fd6-61f6-42fe-8cb7-8bc5fea7fb93",
                column: "concurrency_stamp",
                value: "5b0c97a3-61a6-4db9-b259-5a0ccf8af4cf");

            migrationBuilder.UpdateData(
                table: "asp_net_roles",
                keyColumn: "id",
                keyValue: "f78fff4a-06dc-4b5d-864c-d70cd9ced860",
                column: "concurrency_stamp",
                value: "acd621fd-6ba9-42a8-bf42-ca47ed1fc9e4");
        }
    }
}
