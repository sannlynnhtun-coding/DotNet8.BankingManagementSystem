using DotNet8.BankingManagementSystem.Database.Frontend;
using DotNet8.BankingManagementSystem.Models;
using DotNet8.BankingManagementSystem.Models.Users;

namespace DotNet8.BankingManagementSystem.Backend.Services.Service.Localstorage;

public class UserService
{
    private readonly LocalStorageService _localStorageService;

    public UserService(LocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }

    public async Task<UserResponseModel> CreateUser(UserModel requestModel)
    {
        UserResponseModel model = new UserResponseModel();
        var lst = await _localStorageService.GetUserList(EnumService.Tbl_User.GetKeyName());
        lst ??= new();
        lst.Add(requestModel);
        await _localStorageService.SetUser(lst);
        model.Response = new MessageResponseModel(true, "User has been registered successfully.");
        return model;
    }

    public async Task<UserResponseModel> GetUser(UserModel requestModel)
    {
        UserResponseModel model = new UserResponseModel();
        var lst = await _localStorageService.GetUserList(EnumService.Tbl_User.GetKeyName());
        lst ??= new();
        var item = lst.FirstOrDefault(x => x.UserCode == requestModel.UserCode);
        if (item is null)
        {
            model.Response = new MessageResponseModel(false, "No Data Found.");
            return model;
        }


        model.Data = item;
        model.Response = new MessageResponseModel(true, "Success.");
        return model;
    }

    public async Task<UserResponseModel> UpdateUser(UserModel requestModel)
    {
        UserResponseModel model = new UserResponseModel();
        var lst = await _localStorageService.GetUserList(EnumService.Tbl_User.GetKeyName());
        var result = lst.FirstOrDefault(x => x.UserCode == requestModel.UserCode);
        var index = lst.FindIndex(x => result != null && x.UserCode == result.UserCode);
        if (result is null)
        {
            model.Response = new MessageResponseModel(false, "No Data Found.");
            return model;
        }

        result.UserCode = requestModel.UserCode;
        result.UserName = requestModel.UserName;
        result.FullName = requestModel.FullName;
        result.Email = requestModel.Email;
        result.Nrc = requestModel.Nrc;
        result.MobileNo = requestModel.MobileNo;
        result.Address = requestModel.Address;
        result.StateCode = requestModel.StateCode;
        result.TownshipCode = requestModel.TownshipCode;
        lst[index] = result;

        await _localStorageService.SetUser(lst);
        model.Data = result;
        model.Response = new MessageResponseModel(true, "User has been removed.");
        return model;
    }

    public async Task<UserResponseModel> DeleteUser(UserModel requestModel)
    {
        UserResponseModel model = new UserResponseModel();
        var lst = await _localStorageService.GetUserList(EnumService.Tbl_User.GetKeyName());
        lst ??= new();
        var item = lst.FirstOrDefault(x => x.UserCode == requestModel.UserCode);
        if (item == null)
        {
            model.Response = new MessageResponseModel(false, "No Data Found.");
            return model;
        }

        lst.Remove(item);
        await _localStorageService.SetUser(lst);
        model.Response = new MessageResponseModel(true, "Account has been removed.");
        return model;
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