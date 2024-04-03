namespace DotNet8.BankingManagementSystem.Frontend.Api.Features.User;

public class UserService
{
    private readonly LocalStorageService _localStorageService;

    public UserService(LocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }

    #region Get Users

    public async Task<UserListResponseModel> GetUserList(int pageNo, int pageSize)
    {
        var query = await _localStorageService.GetList<TblUser>(EnumService.Tbl_User.GetKeyName());
        var result = query
            .OrderByDescending(x => x.UserId)
            .Skip((pageNo - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var count = query.Count();
        int pageCount = count / pageSize;
        if (count % pageSize > 0) pageCount++;

        UserListResponseModel model = new UserListResponseModel()
        {
            Data = result.Select(x => x.Change()).ToList(),
            PageSetting = new PageSettingModel(pageNo, pageSize, pageCount),
            Response = new MessageResponseModel(true, "Success")
        };
        return model;
    }

    #endregion
    public async Task<UserResponseModel> CreateUser(UserRequestModel requestModel)
    {
        UserResponseModel model = new UserResponseModel();
        requestModel.UserCode = await GenerateUniqueUserCode();
        var userModel = new TblUser()
        {
            UserName = requestModel.UserName,
            FullName = requestModel.FullName,
            Email = requestModel.Email,
            Address = requestModel.Address,
            MobileNo = requestModel.MobileNo,
            Nrc = requestModel.Nrc,
            StateCode = requestModel.StateCode,
            TownshipCode = requestModel.TownshipCode
        };
        var lst = await _localStorageService.GetList<TblUser>(EnumService.Tbl_User.GetKeyName());
        lst ??= new();
        lst.Add(userModel);
        await _localStorageService.SetList(EnumService.Tbl_User.GetKeyName(), lst);
        model.Response = new MessageResponseModel(true, "User has been registered successfully.");
        return model;
    }

    public async Task<UserResponseModel> GetUserByCode(string userCode)
    {
        UserResponseModel model = new UserResponseModel();
        var lst = await _localStorageService.GetList<TblUser>(EnumService.Tbl_User.GetKeyName());
        lst ??= new();
        var item = lst.FirstOrDefault(x => x.UserCode == userCode);
        if (item is null)
        {
            model.Response = new MessageResponseModel(false, "No Data Found.");
            return model;
        }

        model.Data = item.Change();
        model.Response = new MessageResponseModel(true, "Success.");
        return model;
    }

    public async Task<UserResponseModel> UpdateUser(UserRequestModel requestModel)
    {
        UserResponseModel model = new UserResponseModel();
        var lst = await _localStorageService.GetList<TblUser>(EnumService.Tbl_User.GetKeyName());
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

        await _localStorageService.SetList(EnumService.Tbl_User.GetKeyName(), lst);
        model.Data = result.Change();
        model.Response = new MessageResponseModel(true, "User has been removed.");
        return model;
    }

    public async Task<UserResponseModel> DeleteUser(string userCode)
    {
        UserResponseModel model = new UserResponseModel();
        var lst = await _localStorageService.GetList<TblUser>(EnumService.Tbl_User.GetKeyName());
        lst ??= new();
        var item = lst.FirstOrDefault(x => x.UserCode == userCode);
        if (item == null)
        {
            model.Response = new MessageResponseModel(false, "No Data Found.");
            return model;
        }

        lst.Remove(item);
        await _localStorageService.SetList(EnumService.Tbl_User.GetKeyName(), lst);
        model.Response = new MessageResponseModel(true, "Account has been removed.");
        return model;
    }
    #region Generate user codes

    private async Task<string> GenerateUniqueUserCode()
    {
        string latestUserCode = "AB000";

        if (int.TryParse(latestUserCode.Substring(2), out int numericPart))
        {
            numericPart++;

            while (await IsUserCodeAlreadyUsed("AB" + numericPart.ToString("D3")))
            {
                numericPart++;
            }

            return "AB" + numericPart.ToString("D3");
        }

        return "AB000";
    }

    private async Task<bool> IsUserCodeAlreadyUsed(string userCode)
    {
        var lst = await _localStorageService.GetList<UserModel>(EnumService.Tbl_User.GetKeyName());

        return lst.Any(x => x.UserCode == userCode);
    }

    #endregion
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