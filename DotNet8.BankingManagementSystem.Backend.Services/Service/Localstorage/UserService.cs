using Blazored.LocalStorage;

namespace DotNet8.BankingManagementSystem.Backend.Services.Service.Localstorage;

public class UserService
{
    private readonly ILocalStorageService _localStorageService;

    public UserService(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }

    public async Task CreateUser(UserModel requestModel)
    {
        var lst = await _localStorageService.GetItemAsync<List<UserModel>>("Tbl_User");
        lst ??= new();
        lst.Add(requestModel);
        await _localStorageService.SetItemAsync("Tbl_User", lst);
    }

    public async Task<UserModel> GetUser(UserModel requestModel)
    {
        var lst = await _localStorageService.GetItemAsync<List<UserModel>>("Tbl_User");
        lst ??= new();
        var item = lst.FirstOrDefault(x => x.UserId == requestModel.UserId);
        if (item is null)
        {
            throw new InvalidOperationException();
        }

        return item;
    }

    public async Task<List<UserModel>> GetUserList()
    {
        //bool localStorageName = await JsRuntime.InvokeAsync<bool>("Tbl_Product");
        var lst = await _localStorageService.GetItemAsync<List<UserModel>>("Tbl_Product");
        lst ??= new();
        if (lst.Count != 0)
            return lst.Any()
                ? lst.OrderByDescending(x => x.UserId).ToList()
                : new List<UserModel>();
        var r = new Random();
        var count = 0;
        foreach (var item in GetUser().Distinct())
        {
            count++;
            var user = new UserModel()
            {
                UserId = count,
                // UserName 
                //product_buying_price = r.Next(1000, 100000),
                // product_buying_price = r.Next(10, 100),
                // product_category_code = "PC0001",
                // product_code = "P" + count.ToString().PadLeft(4, '0'),
                // product_creation_date = DateTime.Now,
                // product_id = Guid.NewGuid(),
                // product_name = item,
            };

            lst.Add(user);
        }

        await _localStorageService.SetItemAsync("Tbl_Product", lst);

        return lst.Any()
            ? lst.OrderByDescending(x => x.UserId).ToList()
            : new List<UserModel>();
    }

    #region Usernames

       private string[] GetUser()
    {
        return new[]
        {
            "John", "Jane", "Alice", "Bob", "Charlie", "David", "Emily", "Frank", "Grace", "Henry",
            "Isabella", "Jack", "Kate", "Liam", "Mary", "Nathan", "Olivia", "Peter", "Quinn", "Rachel",
            "Samuel", "Tina", "Ursula", "Victor", "Wendy", "Xander", "Yvonne", "Zachary",
            "Sophia", "Daniel", "Emma", "Michael", "Ella", "James", "Ava", "William", "Mia", "Alexander",
            "Abigail", "Benjamin", "Charlotte", "Ethan", "Harper", "Logan", "Evelyn", "Matthew", "Amelia",
            "Lucas", "Sophie", "Jackson", "Madison", "David", "Chloe", "Joseph", "Emily", "Gabriel",
            "Avery", "Carter", "Lily", "Luke", "Grace", "John", "Zoe", "Ryan", "Natalie", "Elijah",
            "Aria", "Samuel", "Hailey", "Nicholas", "Addison", "Isaac", "Riley", "Owen", "Sofia",
            "Connor", "Eleanor", "Wyatt", "Elizabeth", "Henry", "Penelope", "Sebastian", "Hannah", "Caleb",
            "Aubrey", "Nathan", "Lillian", "Hunter", "Bella", "Christian", "Layla", "Isaiah", "Aurora",
            "Oliver", "Nora", "Lincoln", "Scarlett", "Jonathan", "Aaliyah", "Levi", "Violet", "Jaxon",
            "Stella", "Julian", "Skylar", "Charles", "Lucy", "Aaron", "Claire", "Eli", "Sadie",
            "Thomas", "Anna", "Nathaniel", "Samantha", "Gavin", "Caroline", "Asher", "Genesis", "Adrian",
            "Madelyn", "Robert", "Kennedy", "Hudson", "Allison", "Jordan", "Maya", "Dominic", "Sarah",
            "Jace", "Alexa", "Cooper", "Leah", "Xavier", "Elena", "Kevin", "Ellie", "Parker",
            "Alexandra", "Ian", "Nevaeh", "Chase", "Peyton", "Colton", "Maria", "Diego", "Mackenzie",
            "Jason", "Luna", "Zayden", "Eva", "Xander", "Gabriella", "Ezra", "Adeline", "Brayden",
            "Naomi", "Bentley", "Claire", "Kingston", "Vivian", "Everett", "Alice", "Micah", "Savannah",
            "Ayden", "Audrey", "Ryder", "Josephine", "Tristan", "Gianna", "Brody", "Isabelle", "Declan",
            "Ivy", "Austin", "Lydia", "Gael", "Eliana", "Colin", "Piper", "Elias", "Brielle",
            "Silas", "Ruby", "Braxton", "Jade", "Damian", "Paisley", "Kayden", "Hayden", "Kyle",
            "Eleanor", "Carson", "Willow", "Miles", "Elise", "Kai", "Laila", "Sawyer", "Mya",
            "Camden", "Isabel", "Josiah", "Elliana", "Vincent", "Adalyn", "Calvin", "Nadia", "Maxwell",
            "Evangeline", "Jonah", "Camila", "Grayson", "Esther", "Leon", "Valentina", "Ezekiel", "Hazel",
            "Finn", "Giselle", "George", "Ayla", "Landon", "Melanie", "Eduardo", "Reagan", "Cole",
            "Margaret", "Archer", "Eden", "Leonardo", "Juliana", "Kaden", "Amaya", "Bryson", "Fiona",
            "Riley", "Jasmine", "Cayden", "Jayla", "Emmett", "Brianna", "Hayden", "Delilah", "Bennett",
            "Harmony", "Richard", "Lola", "Ashton", "Summer", "Joel", "Ariana", "Victor", "Leilani",
            "Alexis", "Alexis", "Edward", "Valerie", "Giovanni", "Liliana", "Jude", "Makenzie", "Chance",
            "Adriana", "Tucker", "Alana", "Harrison", "Jocelyn", "Kameron", "Anastasia", "Kaleb", "Angela",
            "Zane", "Daisy", "King", "Phoebe", "Arthur", "Harley", "Theodore", "Isla", "Brooks",
            "Malia", "Caleb", "Brooke", "Roberto", "Juliette", "Dexter", "Tessa", "Beckett", "Hope",
            "Felix", "Serenity", "Payton", "Talia", "Jayce", "Karen", "Ronan", "Kendra", "Angelo",
            "April", "Cristian", "Myla", "Dean", "Julianna", "Ace", "Catherine", "Elliot", "Juliet",
            "Erick", "Sienna", "Nelson", "Daniela", "Knox", "Destiny", "Julius", "Chelsea", "Julio",
            "Camilla", "Marco", "Lorelei", "Jeffrey", "Lia", "Gerardo", "Malaysia", "Ivan", "Raegan",
            "Conner", "Luciana", "Sergio", "Yaretzi", "Mario", "Diana", "Donovan", "Athena", "Marco",
            "Emery", "Leonel", "Sasha", "Emilio", "Nia", "Andres", "Gracelyn", "Derek", "Ruth",
            "Jasper", "Janelle", "Emerson", "Elaina", "Malachi", "Kelly", "Walter", "Kyla", "Zayn",
            "Maddison", "Derrick", "Alivia", "Quinn", "Jayleen", "Gage", "Harper", "Russell",
        };
    }

    #endregion
    
    
 
}