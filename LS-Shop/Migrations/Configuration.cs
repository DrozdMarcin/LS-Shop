using System.Data.Entity.Migrations.Model;
using LS_Shop.Data_Access_Layer;

namespace LS_Shop.Migrations
{
    using LS_Shop.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<LS_Shop.Data_Access_Layer.EfDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "LS-Shop.Data_Access_Layer.ProductsContext";
        }

        protected override void Seed(LS_Shop.Data_Access_Layer.EfDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            var categories = new List<Category>
            {

                new Category()
                {
                    CategoryId = 1,
                    Name = "Batony proteinowe", Description = "Batony proteinowe o różnych smakach oraz gramaturze. Zamiast zjadać wysokokaloryczne batony warto sięgnąć po pyszne batony proteinowe znacznie zdrowsze i korzystniejsze dla organizmu. Wyglądają i smakują jak zwykłe słodycze, a jednak są bogate w składniki odżywcze pomagające w utrzymaniu rezultatów z siłowni.",
                    NameOfImage = "baton.jpg"
                },

                new Category()
                {   CategoryId = 2,
                    Name = "Białko",
                    Description = "Naturalne białko różnego pochodzenia",
                    NameOfImage = "bialko.jpg"
                },

                 new Category()
                {
                     CategoryId = 3,
                    Name = "Carbo", Description = "Węglowodany, odżywki carbo, izotoniki niezbędne każdemu kulturyście, wszystkim trenującym. Szeroki wybór produktów tej kategorii, wysokowartościowe i stuprocentowo oryginalne preparaty. Odżywki typu carbo są łatworozpuszczalnymi źródłami energii występującymi w wielu oryginalnych smakach..",
                    NameOfImage = "carbo.jpg"
                },
                new Category()
                {
                    CategoryId = 4,
                    Name = "Kreatyna", Description = "Kreatyna jest uznawana obecnie za jedną z najpopularniejszych substancji, stosowaną przez kulturystów oraz osoby uprawiające sporty siłowe. Suplementacja kreatyną pozwala w stosunkowo krótkim czasie zwiększyć masę ciała, wpłynąć na budowę mięśni oraz poprawić wytrzymałość, co przekłada się bezpośrednio na lepsze efekty treningu. Właśnie z tego powodu na rynku znajdziemy różnego rodzaju formy kreatyny oraz jej połączenia z innymi składnikami odżywczymi. Do wyboru mamy m.in. jabłczan, monohydrat, ester etylowy, azotan, czy chlorowodorek kreatyny.",
                    NameOfImage = "kreatyna.jpg"
                },
                new Category()
                {
                    CategoryId = 5,
                     Name = "Gainery", Description = "Gainery, powszechnie znane jako odżywki na masę, to wysokokaloryczne preparaty o bardzo wysokiej efektywności. Swoją skuteczność zawdzięczają dwóm elementom: węglowodanom, które stanowią około 70% ich zawartości oraz białku (około 30%). Gainery mogą być dodatkowo wzbogacone o tłuszcze (np. olej MCT), aminokwasy,  witaminy, a nawet wyciągi ziołowe.",
                    NameOfImage = "gainer.jpg"
                },

                new Category()
                {
                    CategoryId = 6,
                    Name = "Glutamina", Description = "Ponieważ organizm potrafi wyprodukować jedynie ograniczoną ilość glutaminy, sportowcy, kulturyści muszą zazwyczaj uzupełniać ją sami. Glutamina jest niezbędna organizmowi do prawidłowego funkcjonowania. Odgrywa ona bardzo ważna rolę w powstawaniu i regeneracji komórek mięśniowych. Dlatego, aby wspomagać się okołotreningowo, wielu kulturystów wybiera właśnie suplementację tym aminokwasem.",
                    NameOfImage = "glutamina.jpg"
                },

                new Category()
                {
                    CategoryId = 7,
                    Name = "HMB", Description = "HMB, czyli beta-hydroksy-beta-metylomaślan stanowi pochodną leucyny - jednego z aminokwasów rozgałęzionych, wchodzącego w skład BCAA. HMB występuje w pożywieniu jedynie w śladowych ilościach (głównie w grejpfrutach oraz niektórych rybach), dlatego jego podstawowym źródłem są suplementy diety (po więcej informacji na temat aminokwasów rozgałęzionych zapraszamy do działu AMINOKWASY BCAA)",
                    NameOfImage = "hmb.jpg"
                },

                  new Category()
                {
                    CategoryId = 8,
                    Name = "Przeciwutleniacze", Description = "Przeciwutleniacze (Antyoksydanty), preparaty przeznaczone dla sportowców oraz osób aktywnych fizycznie. Głównym zadaniem antyoksydantów czyli przeciwutleniaczy jest ochrona przed wolnymi rodnikami,co oznacza po prostu ochronę komórek przed zniszczeniem.",
                    NameOfImage = "przeciwutleniacze.jpg"
                }
            };
            categories.ForEach(c => context.Categories.AddOrUpdate(h => h.Name, c));


            var products = new List<Product>
            {
                /*------------------------------------------ EMPTY 
                  new Product()
                {
                    ProductId = ,
                    Name = " ",
                    Bestseller = false,
                    NameOfImage = ".jpg",
                    Price = 0 ,
                    Description = " ",
                    Hidden = false,
                    DateOfAddition = DateTime.Now,
                    CategoryId = ,
                }

                */

                /* ----------------------------------------- BIAŁKO ID=2 */
                new Product()
                {
                    ProductId = 1,
                    Name = "KFD WPC80 - 750g",
                    Bestseller = true,
                    NameOfImage = "kfdwpc.jpg",
                    Price = 41.00M ,
                    Description = "KFD Premium WPC 80 to wysokiej jakości, instantyzowany i w 100% czysty koncentrat białka serwatkowego użyty jako główny składnik produktu.Wyróżnia się doskonałą kompozycją smakową (w ofercie mamy kilkadziesiąt smaków do wyboru!). Używany przez nas surowiec jest instantyzowany / aglomeryzowany - to znaczy, że charakteryzuje się doskonałą rozpuszczalnością, a po przygotowaniu nie powstaje uporczywa piana. ",
                    Hidden = false,
                    DateOfAddition = DateTime.Now,
                    CategoryId = 2,
                    Quantity = 50
                },

                new Product()
                {
                    ProductId = 2,
                    Name = "KFD X-WHEY - 750g",
                    Bestseller = true,
                    NameOfImage = "kfdxw.jpg",
                    Price = 44.00M ,
                    Description = "X-whey cechuje się także wysoką koncentracją BCAA (aminokwasów rozgałęzionych).Każde z białek dostarcza inną pulę aminokwasów oraz inne frakcje serwatki - glikomakropeptydy, laktoalbuminy, immunoglobuliny. Zestawienie ich razem działa, w opinii wielu osób, skuteczniej, niż stosowanie tych produktów z osobna. Udział procentowy danego rodzaju białka został tak dobrany, aby jak najlepiej wspomagać organizm po każdej aktywności fizycznej. ",
                    Hidden = false,
                    DateOfAddition = DateTime.Now,
                    CategoryId = 2,
                    Quantity = 50
                },

                   new Product()
                {
                    ProductId = 3,
                    Name = "TREC WHEY 100 - 900g ",
                    Bestseller = true,
                    NameOfImage = "tw100.jpg",
                    Price = 66.00M ,
                    Description = "Trec Whey 100 to wysokiej jakości koncentrat białka serwatkowego, który jest źródłem pełnowartościowych protein zwierzęcych. Produkt zawiera wyłącznie białko serwatkowe, bez niepotrzebnych dodatków obniżających zawartość protein w produkcie. Cechuje się niską zawartością tłuszczu i laktozy. Białka serwatki są bogatym źródłem aminokwasów, w tym aminokwasów rozgałęzionych (BCAA), które hamują niekorzystne reakcje kataboliczne oraz źródłem cysteiny, która zwiększa poziom glutationu – silnego antyoksydanta. ",
                    Hidden = false,
                    DateOfAddition = DateTime.Now,
                    CategoryId = 2,
                    Quantity = 50
                },

                   new Product()
                {
                    ProductId = 4,
                    Name = "OLIMP WHEY PROTEIN COMPLEX - 700g",
                    Bestseller = true,
                    NameOfImage = "owpc700.jpg",
                    Price = 52.00M ,
                    Description = "OLIMP Whey Protein Complex 100%®  – specjalnie opracowany matrix białkowy- ultrafiltrowanego koncentratu białek serwatkowych WPC instant oraz izolatu białek serwatkowych WPI-CFM® w formie instant. Pozyskiwane w zaawansowanych procesach technologicznych zachowują pełną strukturę i aktywność funkcjonalnych białek serwatkowych i czynników wzrostowych.",
                    Hidden = false,
                    DateOfAddition = DateTime.Now,
                    CategoryId = 2,
                    Quantity = 50
                },
                  

             /* ----------------------------------------- BATONY ID=1 */

         new Product()
                {
                    ProductId = 5,
                    Name = "OLIMP MATRIX BATON - 80g ",
                    Bestseller = true,
                    NameOfImage = "omb80.jpg",
                    Price = 11.00M ,
                    Description = "MATRIX pro 32 to wysokobiałkowy baton „low carb” typu „Meal Replacement” o smaku aromatycznej czekolady w polewie czekoladowej. Zawiera dużą dawkę białka m.in. w potaci izolatu białka serwatkowego (WPI), który charakteryzuje się wysoką zawartością protein oraz aminokwasów, niską kalorycznością i niską zawartością tłuszczu. Produkt został wzbogacony o kompleks witamin i minerałów, niezbędnych do prawidłowego funkcjonowania organizmu. Słodki smak zawdzięcza sukralozie – substancji nawet 800-krotnie słodszej od cukru i nie dostarczającej kalorii. ",
                    Hidden = false,
                    DateOfAddition = DateTime.Now,
                    CategoryId = 1,
                    Quantity = 50
                },

             new Product()
                {
                    ProductId = 6,
                    Name = "TREC BOOSTER - 100g ",
                    Bestseller = true,
                    NameOfImage = "tb100.jpg",
                    Price = 6.00M ,
                    Description = "Booster to białkowo-węglowodanowy baton, stworzony w oparciu o białko serwatki. Tłuszcze znajdujące się w produkcie pochodzą w 100% tylko z naturalnych surowców użytych do produkcji batonu - orzechów arachidowych oraz miazgi kakaowej. ",
                    Hidden = false,
                    DateOfAddition = DateTime.Now,
                    CategoryId = 1,
                    Quantity = 100
                },

                 new Product()
                {
                    ProductId = 7,
                    Name = "OPTIMUM WHIPPED BITES - 76g",
                    Bestseller = false,
                    NameOfImage = "owb76.jpg",
                    Price = 8.00M ,
                    Description = " Pyszny baton od firmy OPTIMUM NUTRITIUN to koncentracja smaku w formie smakołyka,który możesz mieć zawsze przy sobie. Produkt zawiera dużo białka (20g w porcji) oraz błonnik pokarmowy. Dzięki obniżonej zawartości cukru, nadaje się do stosowania dla osób, które szczególnie dbają o szczupłą sylwetkę. Protein Whipped Bites z OPTIMUM NUTRITION są świetną przekąską do podjadania w dowolnym czasie, bez wyrzutów sumienia. Produkt jest dostępny w 3 przepysznych smakach: Słonym Karmelu, Czekoladzie, Truskawce ze śmietanką.",
                    Hidden = false,
                    DateOfAddition = DateTime.Now,
                    CategoryId = 1,
                    Quantity = 20
                },

                 /*---------------------------------------- CARBO ID=3  */
                 
                     new Product()
                {
                    ProductId = 8,
                    Name = "KFD PREMIUM X-CARBS - 1000g",
                    Bestseller = true,
                    NameOfImage = "kfdpxc.jpg",
                    Price = 18.00M ,
                    Description = "KFD Premium Carb Mix to unikalna mieszanka wysokiej jakości węglowodanów – mono-, poli- i oligosacharydów w postaci: dekstrozy, maltodekstryny i woskowej skrobi kukurydzianej – waxy maize. Takie połączenie wpływa na rozłożoną w czasie absorpcję węglowodanów. Prostsze formy przyswajane są szybciej, a bardziej złożone wchłaniają się stopniowo. ",
                    Hidden = false,
                    DateOfAddition = DateTime.Now,
                    CategoryId = 3,
                    Quantity = 10
                },

                    new Product()
                {
                    ProductId = 9,
                    Name = "OLIMP ISO PLUS SPORT DRINK POWDER - 700g",
                    Bestseller = false,
                    NameOfImage = "oip.jpg",
                    Price = 66.00M ,
                    Description = "Koncentrat napoju Olimp Iso Plus Sport Drink Powder przeznaczony jest do sporządzania izotonicznego napoju poprzez rozpuszczenie w przegotowanej, ostudzonej wodzie. Produkt został wzbogacony o L-karnitynę, L-glutaminę oraz witaminy.  ",
                    Hidden = false,
                    DateOfAddition = DateTime.Now,
                    CategoryId = 3,
                    Quantity = 10
                },

                /*---------------------------------------- KREATYNA ID=4  */

                   new Product()
                {
                    ProductId = 10,
                    Name = "KFD PREMIUM CREATINE - 500g ",
                    Bestseller = false,
                    NameOfImage = "kfdkpc.jpg",
                    Price = 23.00M ,
                    Description = "KFD Premium Creatine to suplement diety zawierający w 100% czysty monohydrat kreatyny 200 mesh.Podstawową funkcją kreatyny jest produkcja energii, która wykorzystywana jest przez organizm m.in. na odbudowanie zapasów energetycznych organizmu. Przyczynia się to więc do regeneracji mięśni(kreatyna ma także działanie antykataboliczne) i pomaga ćwiczyć dłużej, ale równie skutecznie.Uważana jest za najbardziej skuteczny, legalny środek ze względu na najbardziej efektywny przyrost siły oraz beztłuszczowej masy ciała w relatywnie krótkim czasie.",
                    Hidden = false,
                    DateOfAddition = DateTime.Now,
                    CategoryId = 4,
                    Quantity = 50
                },
                   new Product()
                {
                    ProductId = 11,
                    Name = "TREC CM3 - 180 KAPS",
                    Bestseller = false,
                    NameOfImage = "treccm3.jpg",
                    Price = 41.00M ,
                    Description = "Trec Nutrition CM3 to preparat zawierający krystalicznie czysty jabłczan kreatyny  (Tri-Creatine Malate) w zwiększonej dawce. Jabłczan kreatyny powstaje w wyniku zaawansowanego procesu produkcyjnego przez połączenie 3 cząsteczek monohydratu kreatyny z 1 kwasu jabłkowego. ",
                    Hidden = false,
                    DateOfAddition = DateTime.Now,
                    CategoryId = 4,
                    Quantity = 50
                },
                   new Product()
                {
                    ProductId = 12,
                    Name = "OLIMP CREATINE XPLODE - 500g ",
                    Bestseller = false,
                    NameOfImage = "ocx.jpg",
                    Price = 69.00M ,
                    Description = "Creatine Xplode™ to zestawienie 6 form kreatyn, które cechują się wysoką aktywnością anaboliczną. W skład Creatine Xplode™ wchodzą takie formy kreatyny jak jabłczan, ester etylowy, alfa-ketoglutaran, pirogronian, cytrynian oraz chelat kreatynowy magnezu (Creatine Magna Power). Produkt został urozmaicony o taurynę, która pomaga transportować kreatynę.  ",
                    Hidden = false,
                    DateOfAddition = DateTime.Now,
                    CategoryId = 4,
                    Quantity = 50
                },

                /*---------------------------------------- GAINERY ID=5  */

                   new Product()
                {
                    ProductId = 13,
                    Name = "KFD PREMIUM X-GAINER - 1000g",
                    Bestseller = false,
                    NameOfImage = "kfdpxg1000.jpg",
                    Price = 26.00M ,
                    Description = "UWAGA! Smaki czekoladowe - różnica w cenie wynika z różnicy w podatku VAT produktów zawierających kakao (zwykłe smaki - 8% VAT, z kakao - aż 23%).Produkt, w przeciwieństwie do większości tego typu, które zawierają wyłącznie cukier, zawiera niezwykle cenne źródło węglowodanów w postaci zmielonych płatków owsianych(płatki się nie rozpuszczą tak jak rozpuszcza się zwykły cukier).KFD Premium X - Gainer to mieszanka najwyższej jakości węglowodanów(w tym zmielonych płatków owsianych I gatunku) oraz dwóch rodzajów białka -serwatkowego(WPC) i kazeiny micelarnej.Produkt został dodatkowo wzbogacony o Beta-Alaninę oraz L-Glutaminę, co może zainteresować osoby myślące o regeneracji potreningowej.",
                    Hidden = false,
                    DateOfAddition = DateTime.Now,
                    CategoryId =5 ,
                    Quantity = 10
                },

                   new Product()
                {
                    ProductId = 14,
                    Name = "SCITEC JUMBO - 4400g ",
                    Bestseller = false,
                    NameOfImage = "sj4000.jpg",
                    Price = 179.00M ,
                    Description = "Jumbo to odżywka węglowodanowo-białkowa typu gainer. Jedna porcja dostarcza 50g farmaceutycznej jakości protein oraz 6 rodzajów węglowodanów o różnym czasie wchłaniania. Produkt nie zawiera żadnych słodzików",
                    Hidden = false,
                    DateOfAddition = DateTime.Now,
                    CategoryId = 5,
                    Quantity = 10
                },
                /*---------------------------------------- GLUTAMINA ID=6  */
                    new Product()
                {
                    ProductId = 15,
                    Name = "KFD PREMIUM GLUTAMINE - 500g",
                    Bestseller = false,
                    NameOfImage = "pfdg.jpg",
                    Price = 44.00M ,
                    Description = "KFD Premium Glutamine to produkt zawierający czystą L-Glutaminę w wersji smakowej.L-glutamina jest aminokwasem, który bierze udział w wielu procesach metabolicznych. Jest  stosowana jako suplement diety zarówno wśród kulturystów czy pasjonatów sportów wytrzymałościowych, jak i osoby cierpiące na skurcze mięśni. Jej głównym zadaniem jest uzupełnienie zapasu aminokwasów. [ ",
                    Hidden = false,
                    DateOfAddition = DateTime.Now,
                    CategoryId = 6,
                    Quantity = 50
                },
                   new Product()
                {
                    ProductId = 16,
                    Name = "OLIMP GLUTAMINE XPLODE - 500g",
                    Bestseller = false,
                    NameOfImage = "ogxp.jpg",
                    Price = 67.00M ,
                    Description = "Glutamine Xplode to suplement diety zawierający glutaminę, wzbogacony o innowacyjną formułę VIT-A-MIN SuperCharge . W skład preparatu wchodzi: L-leucyna, L-cysteina, selen, witamina C (PureWay-C®), witaminy z grupy B: witamina B6, B12, kwas foliowy i niacyna.",
                    Hidden = false,
                    DateOfAddition = DateTime.Now,
                    CategoryId = 6,
                    Quantity = 50
                },

                  /*---------------------------------------- HMB ID=7  */
                   new Product()
                {
                    ProductId = 17,
                    Name = " HI TEC HMB - 400 KAPS",
                    Bestseller = false,
                    NameOfImage = "hthmb.jpg",
                    Price = 89.00M ,
                    Description = "Hi Tec HMB w kapsułkach to wysokiej jakości antykatabolik w postaci kwasu  β-hydroksy β-metylomasłowego. Preparat przeznaczony dla osób, które myślą o wymodelowaniu sylwetki, ukształtowaniu pięknej rzeźby ciała i zredukowaniu tkanki tłuszczowej.Kwas  β-hydroksy β-metylomasłowy (HMB) powstaje w efekcie katabolizmu białek – jest produktem metabolizmu leucyny, która zostaje uwolniona z białek mięśniowych na drodze proteolizy ",
                    Hidden = false,
                    DateOfAddition = DateTime.Now,
                    CategoryId = 7,
                    Quantity = 50
                },
                    new Product()
                {
                    ProductId = 18,
                    Name = "TREC HMB LIQUID - 100 ML",
                    Bestseller = false,
                    NameOfImage = "thmb.jpg",
                    Price = 129.00M ,
                    Description = "HMB (B-hydroxy B-metylomaślan) jest krótkołańcuchowym kwasem tłuszczowym, który powstaje w organizmie z jednego z trzech aminokwasów rozgałęzionych BCAA – leucyny. HMB w formie płynnej charakteryzuje się najwyższym stężeniem czystego HMB od 95% do 99%. Ma ona postać gęstego i bardzo kwaśnego płynu. ",
                    Hidden = false,
                    DateOfAddition = DateTime.Now,
                    CategoryId = 7,
                    Quantity = 50
                },

                     /*---------------------------------------- Przeciwutleniacze ID=8  */
                   new Product()
                {
                    ProductId = 19,
                    Name = "SWANSON 300 MG - 60 KAPS",
                    Bestseller = false,
                    NameOfImage = "swason.jpg",
                    Price = 24.00M ,
                    Description = "Suplement diety Swanson ALA dostarcza 300 mg kwasu alfa-liponowego w jednej kapsułce. Kwas alfa-liponowy jest naturalnie produkowany przez ludzki organizm. Jest silnymi antyoksydantem, który z jednej strony wykazuje zdolność do wymiatania wolnych rodników, z drugiej zapobiega niedoborom witaminy C i witaminy E[1] Redukuje stres oksydacyjny wywołany wysiłkiem fizycznym. Ponadto, zmniejsza ryzyko rozwoju miażdżycy i hamuje rozwój już istniejącej blaszki miażdżycowej.Kwas alfa-liponowy ma zdolność chelatowania jonów metali.Uczestniczy w wielu szlakach metabolicznych insuliny, wychwytywaniu glukozy i syntezie glikogenu. Pozytywnie wpływa na pracę układu krwionośnego i serca ",
                    Hidden = false,
                    DateOfAddition = DateTime.Now,
                    CategoryId = 8,
                    Quantity = 50
                }
            };
            products.ForEach(p => context.Products.AddOrUpdate(h => h.Name, p));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            roleManager.Create(new IdentityRole("Administrator"));
            roleManager.Create(new IdentityRole("Employee"));
            roleManager.Create(new IdentityRole("User"));

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var user = new ApplicationUser
            {
                Email = "admin@123.aa",
                UserName = "admin@123.aa",
                UserData = new UserData()
            };
            if (userManager.FindByEmail("admin@123.aa") == null)
                userManager.Create(user, "ZAQ!2wsx");


            var adminUserRole = new IdentityUserRole()
            {
                RoleId = roleManager.FindByName("Administrator").Id,
                UserId = userManager.FindByEmail("admin@123.aa").Id
            };
            if (!roleManager.FindByName("Administrator").Users.Where(o => o.UserId == userManager.FindByEmail("admin@123.aa").Id).Any())
                roleManager.FindByName("Administrator").Users.Add(adminUserRole);

            context.Settings.Add(new Settings() { Id = 1, QuantityOfProductsLimit = 0, QuantityOfCanceledOrdersLimit = 0 });

            context.SaveChanges();
        }
    }
}
