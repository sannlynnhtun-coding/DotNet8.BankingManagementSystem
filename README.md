# Banking Management System

Summary 
> ငွေသွင်း ငွေလွှဲ ပြုလုပ်သည့်အခါ ဘဏ် staff ကနေ အသုံးပြုရတဲ့ စနစ်ဖြစ်တယ်။ ကိုယ်‌‌ ငွေလွှဲချင်တဲ့ account နံပါတ်ကို ဘဏ် staff အားပြောပြပြီးတော့မှ ဘဏ် staff ကနေ တစ်ဆင့် ငွေသွင်း ငွေလွှဲ‌ ပြုလုပ်ပေးနိုင်တဲ့  Service မျိုး ဖြစ်ပါတယ်။



ပါ၀င်မည့် Menu များကတော့
- General Setup
  - [State List](#state-and-township-list)
  - [Township List](#state-and-township-list)
- Account
  - [Create Account](#create-account)
  - [Account List](#account-list)
- Report
  - [Transaction History](#transaction-history)
- Transaction
  - [Transfer](#transfer)
  - [Withdraw](#withdraw)
  - [Deposit](#deposit)
  

### State and Township List
State And Township Data တွေကို ဤ [Link](https://themimu.info/place-codes) ကနေ ထုတ်ယူထားတယ်။ Account ဖွင့်ရာတွင်အသုံးပြုတယ်။ ဘယ်နေရာ ဒေသမှာ ဖွင့်ထားသလဲဆိုတာ သိဖို့အတွက် ဖြစ်တယ်။

### Create Account
ဘဏ်တွင် ငွေသွင်း ငွေလွှဲ service များကိုအသုံးပြုလိုပါက ဘဏ်တွင် account ဖွင့်ရပါမည်။


### Account List

ဘဏ်တွင် customer များဖွင့်ခဲ့သော account များကိုကြည့်ရှုလို့ရမယ်။


### Transaction History

Customer ငွေလွှဲသွားတဲ့ account တွေကို Date အလိုက်ကြည့်ရှုလို့ရတဲ့ Report ဖြစ်ပါတယ်။

### Transfer

Customer ငွေလွှဲချင်သည့် အကောင့်ထဲသို့ ဘဏ် Staff မှ တစ်ဆင့် လွှဲပြောင်းပေးနိုင်ပါသည်။


### Withdraw

Customer Account ထဲမှ ငွေ Amount တွေကိုထုတ်ယူလို့ရပါမည်။


### Deposit

Customer Account ထဲသို့ ငွေ Amount တွေကိုထည့်သွင်းလို့ရနိုင်ပါသည်။

-----------




CRUD
List (Pagination)
	- EFCore
	- Dapper
Create
	- Validation (Check Required Fields)
	- Create
	- Success / Fail
	- List
Update
	- Validation (Check Required Fields)
	- Update
	- Success / Fail
	- List
Delete
	- Yes/No
	- Delete

	https://github.com/muratoner/blazornotiflix
```
BackendApi
- Controller
	- Request Model
		- Ok
		- Internal Server Error
- Service
	- Logic
		- CRUD
		- Request Model => AppDbContext (DTO) Data Tranfer Object [StateRequestModel - TblState]
		- Return
			- IsSuccess (true, false ?? message)
			- Message
			- Read (List)
			- Edit (Item)
			- Create, Update, Delete


Models
	- StateModel (item)
	- StateRequestModel (StateId, StateCode, StateName)
	- StateResponseModel : IsSuccess, Message
		- Item
	- StateListResponseModel : IsSuccess, Message
		- List

Mapper
	- StateRequestModel - TblState
	- TblState - StateModel
	- StateModel - TblState
```

```bash
dotnet ef dbcontext scaffold "server=.;database=BankingManagementSystem;user id=sa;password=sasa@123;Trust Server Certificate=true;MultipleActiveResultSets=True;", Microsoft.EntityFrameworkCore.SqlServer -o EfAppDbContextModels -c AppDbContext -f
```




```sql
select * from [dbo].[Tbl_PlaceTownship] t
inner join [dbo].[Tbl_PlaceState] s on s.StateCode = t.StateCode
```

```sql

USE [BankingManagementSystem]
GO
/****** Object:  Table [dbo].[Tbl_Account]    Script Date: 3/11/2024 11:16:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Account](
	[AccountId] [int] IDENTITY(1,1) NOT NULL,
	[AccountNo]  AS (right('000000'+CONVERT([varchar](6),[AccountId]),(6))),
	[CustomerCode] [nvarchar](50) NOT NULL,
	[Balance] [decimal](20, 2) NOT NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[AccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_AdminUser]    Script Date: 3/11/2024 11:16:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_AdminUser](
	[AdminUserId] [int] IDENTITY(1,1) NOT NULL,
	[AdminUserCode] [nvarchar](50) NOT NULL,
	[AdminUserName] [nvarchar](50) NOT NULL,
	[MobileNo] [nvarchar](15) NOT NULL,
	[UserRoleCode] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Tbl_Employee] PRIMARY KEY CLUSTERED 
(
	[AdminUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_PlaceState]    Script Date: 3/11/2024 11:16:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_PlaceState](
	[StateId] [int] IDENTITY(1,1) NOT NULL,
	[StateCode] [nvarchar](50) NOT NULL,
	[StateName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Tbl_City] PRIMARY KEY CLUSTERED 
(
	[StateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_PlaceTownship]    Script Date: 3/11/2024 11:16:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_PlaceTownship](
	[TownshipId] [int] IDENTITY(1,1) NOT NULL,
	[TownshipCode] [nvarchar](50) NOT NULL,
	[TownshipName] [nvarchar](50) NOT NULL,
	[StateCode] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Tbl_Township] PRIMARY KEY CLUSTERED 
(
	[TownshipId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_TransactionHistory]    Script Date: 3/11/2024 11:16:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_TransactionHistory](
	[TransactionHistoryId] [int] IDENTITY(1,1) NOT NULL,
	[FromAccountNo] [nvarchar](50) NOT NULL,
	[ToAccountNo] [nvarchar](50) NOT NULL,
	[TransactionDate] [datetime] NOT NULL,
	[Amount] [decimal](20, 2) NOT NULL,
	[AdminUserCode] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Tbl_Transfer] PRIMARY KEY CLUSTERED 
(
	[TransactionHistoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_User]    Script Date: 3/11/2024 11:16:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserCode] [nvarchar](50) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[FullName] [nvarchar](50) NOT NULL,
	[MobileNo] [nvarchar](15) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Nrc] [nvarchar](50) NOT NULL,
	[Address] [nvarchar](200) NOT NULL,
	[StateCode] [nvarchar](50) NOT NULL,
	[TownshipCode] [nvarchar](50) NOT NULL,
	[CustomerId]  AS ('C'+right('000000'+CONVERT([varchar](6),[UserId]),(6))) PERSISTED,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Tbl_Account] ON 

INSERT [dbo].[Tbl_Account] ([AccountId], [CustomerCode], [Balance]) VALUES (1, N'C0000001', CAST(1.00 AS Decimal(20, 2)))
SET IDENTITY_INSERT [dbo].[Tbl_Account] OFF
GO
SET IDENTITY_INSERT [dbo].[Tbl_PlaceState] ON 

INSERT [dbo].[Tbl_PlaceState] ([StateId], [StateCode], [StateName]) VALUES (1, N'MMR001', N'Kachin')
INSERT [dbo].[Tbl_PlaceState] ([StateId], [StateCode], [StateName]) VALUES (2, N'MMR002', N'Kayah')
INSERT [dbo].[Tbl_PlaceState] ([StateId], [StateCode], [StateName]) VALUES (3, N'MMR003', N'Kayin')
INSERT [dbo].[Tbl_PlaceState] ([StateId], [StateCode], [StateName]) VALUES (4, N'MMR004', N'Chin')
INSERT [dbo].[Tbl_PlaceState] ([StateId], [StateCode], [StateName]) VALUES (5, N'MMR005', N'Sagaing')
INSERT [dbo].[Tbl_PlaceState] ([StateId], [StateCode], [StateName]) VALUES (6, N'MMR006', N'Tanintharyi')
INSERT [dbo].[Tbl_PlaceState] ([StateId], [StateCode], [StateName]) VALUES (7, N'MMR007', N'Bago (East)')
INSERT [dbo].[Tbl_PlaceState] ([StateId], [StateCode], [StateName]) VALUES (8, N'MMR008', N'Bago (West)')
INSERT [dbo].[Tbl_PlaceState] ([StateId], [StateCode], [StateName]) VALUES (9, N'MMR009', N'Magway')
INSERT [dbo].[Tbl_PlaceState] ([StateId], [StateCode], [StateName]) VALUES (10, N'MMR010', N'Mandalay')
INSERT [dbo].[Tbl_PlaceState] ([StateId], [StateCode], [StateName]) VALUES (11, N'MMR011', N'Mon')
INSERT [dbo].[Tbl_PlaceState] ([StateId], [StateCode], [StateName]) VALUES (12, N'MMR012', N'Rakhine')
INSERT [dbo].[Tbl_PlaceState] ([StateId], [StateCode], [StateName]) VALUES (13, N'MMR013', N'Yangon')
INSERT [dbo].[Tbl_PlaceState] ([StateId], [StateCode], [StateName]) VALUES (14, N'MMR014', N'Shan (South)')
INSERT [dbo].[Tbl_PlaceState] ([StateId], [StateCode], [StateName]) VALUES (15, N'MMR015', N'Shan (North)')
INSERT [dbo].[Tbl_PlaceState] ([StateId], [StateCode], [StateName]) VALUES (16, N'MMR016', N'Shan (East)')
INSERT [dbo].[Tbl_PlaceState] ([StateId], [StateCode], [StateName]) VALUES (17, N'MMR017', N'Ayeyarwady')
INSERT [dbo].[Tbl_PlaceState] ([StateId], [StateCode], [StateName]) VALUES (18, N'MMR018', N'Nay Pyi Taw')
INSERT [dbo].[Tbl_PlaceState] ([StateId], [StateCode], [StateName]) VALUES (19, N'MMR111', N'Bago')
INSERT [dbo].[Tbl_PlaceState] ([StateId], [StateCode], [StateName]) VALUES (20, N'MMR222', N'Shan')
SET IDENTITY_INSERT [dbo].[Tbl_PlaceState] OFF
GO
SET IDENTITY_INSERT [dbo].[Tbl_PlaceTownship] ON 

INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (2, N'MMR017022', N'Danubyu', N'MMR017')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (3, N'MMR017026', N'Dedaye', N'MMR017')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (4, N'MMR017015', N'Einme', N'MMR017')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (5, N'MMR017008', N'Hinthada', N'MMR017')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (6, N'MMR017013', N'Ingapu', N'MMR017')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (7, N'MMR017002', N'Kangyidaunt', N'MMR017')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (8, N'MMR017025', N'Kyaiklat', N'MMR017')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (9, N'MMR017012', N'Kyangin', N'MMR017')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (10, N'MMR017007', N'Kyaunggon', N'MMR017')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (11, N'MMR017005', N'Kyonpyaw', N'MMR017')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (12, N'MMR017016', N'Labutta', N'MMR017')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (13, N'MMR017010', N'Lemyethna', N'MMR017')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (14, N'MMR017019', N'Maubin', N'MMR017')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (15, N'MMR017018', N'Mawlamyinegyun', N'MMR017')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (16, N'MMR017011', N'Myanaung', N'MMR017')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (17, N'MMR017014', N'Myaungmya', N'MMR017')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (18, N'MMR017004', N'Ngapudaw', N'MMR017')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (19, N'MMR017021', N'Nyaungdon', N'MMR017')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (20, N'MMR017020', N'Pantanaw', N'MMR017')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (21, N'MMR017001', N'Pathein', N'MMR017')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (22, N'MMR017023', N'Pyapon', N'MMR017')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (23, N'MMR017003', N'Thabaung', N'MMR017')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (24, N'MMR017017', N'Wakema', N'MMR017')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (25, N'MMR017006', N'Yegyi', N'MMR017')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (26, N'MMR017009', N'Zalun', N'MMR017')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (27, N'MMR007001', N'Bago', N'MMR007')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (28, N'MMR007007', N'Daik-U', N'MMR007')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (29, N'MMR007014', N'Htantabin', N'MMR007')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (30, N'MMR007003', N'Kawa', N'MMR007')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (31, N'MMR007011', N'Kyaukkyi', N'MMR007')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (32, N'MMR007006', N'Kyauktaga', N'MMR007')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (33, N'MMR007005', N'Nyaunglebin', N'MMR007')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (34, N'MMR007013', N'Oktwin', N'MMR007')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (35, N'MMR007012', N'Phyu', N'MMR007')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (36, N'MMR007008', N'Shwegyin', N'MMR007')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (37, N'MMR007009', N'Taungoo', N'MMR007')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (38, N'MMR007002', N'Thanatpin', N'MMR007')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (39, N'MMR007004', N'Waw', N'MMR007')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (40, N'MMR007010', N'Yedashe', N'MMR007')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (41, N'MMR008014', N'Gyobingauk', N'MMR008')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (42, N'MMR008008', N'Letpadan', N'MMR008')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (43, N'MMR008009', N'Minhla', N'MMR008')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (44, N'MMR008013', N'Monyo', N'MMR008')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (45, N'MMR008012', N'Nattalin', N'MMR008')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (46, N'MMR008010', N'Okpho', N'MMR008')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (47, N'MMR008003', N'Padaung', N'MMR008')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (48, N'MMR008002', N'Paukkhaung', N'MMR008')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (49, N'MMR008004', N'Paungde', N'MMR008')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (50, N'MMR008001', N'Pyay', N'MMR008')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (51, N'MMR008006', N'Shwedaung', N'MMR008')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (52, N'MMR008007', N'Thayarwady', N'MMR008')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (53, N'MMR008005', N'Thegon', N'MMR008')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (54, N'MMR008011', N'Zigon', N'MMR008')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (55, N'MMR004001', N'Falam', N'MMR004')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (56, N'MMR004002', N'Hakha', N'MMR004')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (57, N'MMR004008', N'Kanpetlet', N'MMR004')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (58, N'MMR004007', N'Matupi', N'MMR004')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (59, N'MMR004006', N'Mindat', N'MMR004')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (60, N'MMR004009', N'Paletwa', N'MMR004')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (61, N'MMR004004', N'Tedim', N'MMR004')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (62, N'MMR004003', N'Thantlang', N'MMR004')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (63, N'MMR004005', N'Tonzang', N'MMR004')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (64, N'MMR001010', N'Bhamo', N'MMR001')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (65, N'MMR001005', N'Chipwi', N'MMR001')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (66, N'MMR001009', N'Hpakant', N'MMR001')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (67, N'MMR001003', N'Injangyang', N'MMR001')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (68, N'MMR001018', N'Khaunglanhpu', N'MMR001')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (69, N'MMR001016', N'Machanbaw', N'MMR001')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (70, N'MMR001013', N'Mansi', N'MMR001')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (71, N'MMR001008', N'Mogaung', N'MMR001')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (72, N'MMR001007', N'Mohnyin', N'MMR001')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (73, N'MMR001012', N'Momauk', N'MMR001')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (74, N'MMR001001', N'Myitkyina', N'MMR001')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (75, N'MMR001017', N'Nawngmun', N'MMR001')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (76, N'MMR001014', N'Puta-O', N'MMR001')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (77, N'MMR001011', N'Shwegu', N'MMR001')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (78, N'MMR001015', N'Sumprabum', N'MMR001')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (79, N'MMR001004', N'Tanai', N'MMR001')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (80, N'MMR001006', N'Tsawlaw', N'MMR001')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (81, N'MMR001002', N'Waingmaw', N'MMR001')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (82, N'MMR002005', N'Bawlake', N'MMR002')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (83, N'MMR002002', N'Demoso', N'MMR002')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (84, N'MMR002006', N'Hpasawng', N'MMR002')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (85, N'MMR002003', N'Hpruso', N'MMR002')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (86, N'MMR002001', N'Loikaw', N'MMR002')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (87, N'MMR002007', N'Mese', N'MMR002')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (88, N'MMR002004', N'Shadaw', N'MMR002')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (89, N'MMR003002', N'Hlaingbwe', N'MMR003')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (90, N'MMR003001', N'Hpa-An', N'MMR003')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (91, N'MMR003003', N'Hpapun', N'MMR003')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (92, N'MMR003006', N'Kawkareik', N'MMR003')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (93, N'MMR003007', N'Kyainseikgyi', N'MMR003')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (94, N'MMR003005', N'Myawaddy', N'MMR003')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (95, N'MMR003004', N'Thandaunggyi', N'MMR003')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (96, N'MMR009016', N'Aunglan', N'MMR009')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (97, N'MMR009003', N'Chauk', N'MMR009')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (98, N'MMR009023', N'Gangaw', N'MMR009')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (99, N'MMR009015', N'Kamma', N'MMR009')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (100, N'MMR009001', N'Magway', N'MMR009')
GO
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (101, N'MMR009007', N'Minbu', N'MMR009')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (102, N'MMR009014', N'Mindon', N'MMR009')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (103, N'MMR009013', N'Minhla', N'MMR009')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (104, N'MMR009020', N'Myaing', N'MMR009')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (105, N'MMR009005', N'Myothit', N'MMR009')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (106, N'MMR009006', N'Natmauk', N'MMR009')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (107, N'MMR009009', N'Ngape', N'MMR009')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (108, N'MMR009018', N'Pakokku', N'MMR009')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (109, N'MMR009021', N'Pauk', N'MMR009')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (110, N'MMR009008', N'Pwintbyu', N'MMR009')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (111, N'MMR009010', N'Salin', N'MMR009')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (112, N'MMR009025', N'Saw', N'MMR009')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (113, N'MMR009022', N'Seikphyu', N'MMR009')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (114, N'MMR009011', N'Sidoktaya', N'MMR009')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (115, N'MMR009017', N'Sinbaungwe', N'MMR009')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (116, N'MMR009004', N'Taungdwingyi', N'MMR009')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (117, N'MMR009012', N'Thayet', N'MMR009')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (118, N'MMR009024', N'Tilin', N'MMR009')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (119, N'MMR009002', N'Yenangyaung', N'MMR009')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (120, N'MMR009019', N'Yesagyo', N'MMR009')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (121, N'MMR010006', N'Amarapura', N'MMR010')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (122, N'MMR010001', N'Aungmyaythazan', N'MMR010')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (123, N'MMR010002', N'Chanayethazan', N'MMR010')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (124, N'MMR010004', N'Chanmyathazi', N'MMR010')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (125, N'MMR010020', N'Kyaukpadaung', N'MMR010')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (126, N'MMR010013', N'Kyaukse', N'MMR010')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (127, N'MMR010009', N'Madaya', N'MMR010')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (128, N'MMR010003', N'Mahaaungmyay', N'MMR010')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (129, N'MMR010029', N'Mahlaing', N'MMR010')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (130, N'MMR010028', N'Meiktila', N'MMR010')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (131, N'MMR010011', N'Mogoke', N'MMR010')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (132, N'MMR010017', N'Myingyan', N'MMR010')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (133, N'MMR010015', N'Myittha', N'MMR010')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (134, N'MMR010019', N'Natogyi', N'MMR010')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (135, N'MMR010021', N'Ngazun', N'MMR010')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (136, N'MMR010022', N'Nyaung-U', N'MMR010')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (137, N'MMR010007', N'Patheingyi', N'MMR010')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (138, N'MMR010024', N'Pyawbwe', N'MMR010')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (139, N'MMR010005', N'Pyigyitagon', N'MMR010')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (140, N'MMR010008', N'Pyinoolwin', N'MMR010')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (141, N'MMR010010', N'Singu', N'MMR010')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (142, N'MMR010014', N'Sintgaing', N'MMR010')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (143, N'MMR010016', N'Tada-U', N'MMR010')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (144, N'MMR010018', N'Taungtha', N'MMR010')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (145, N'MMR010012', N'Thabeikkyin', N'MMR010')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (146, N'MMR010030', N'Thazi', N'MMR010')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (147, N'MMR010031', N'Wundwin', N'MMR010')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (148, N'MMR010023', N'Yamethin', N'MMR010')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (149, N'MMR011010', N'Bilin', N'MMR011')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (150, N'MMR011003', N'Chaungzon', N'MMR011')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (151, N'MMR011002', N'Kyaikmaraw', N'MMR011')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (152, N'MMR011009', N'Kyaikto', N'MMR011')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (153, N'MMR011001', N'Mawlamyine', N'MMR011')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (154, N'MMR011005', N'Mudon', N'MMR011')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (155, N'MMR011008', N'Paung', N'MMR011')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (156, N'MMR011004', N'Thanbyuzayat', N'MMR011')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (157, N'MMR011007', N'Thaton', N'MMR011')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (158, N'MMR011006', N'Ye', N'MMR011')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (159, N'MMR018004', N'Det Khi Na Thi Ri', N'MMR018')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (160, N'MMR018007', N'Lewe', N'MMR018')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (161, N'MMR018008', N'Oke Ta Ra Thi Ri', N'MMR018')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (162, N'MMR018005', N'Poke Ba Thi Ri', N'MMR018')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (163, N'MMR018006', N'Pyinmana', N'MMR018')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (164, N'MMR018003', N'Tatkon', N'MMR018')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (165, N'MMR018002', N'Za Bu Thi Ri', N'MMR018')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (166, N'MMR018001', N'Zay Yar Thi Ri', N'MMR018')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (167, N'MMR012014', N'Ann', N'MMR012')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (168, N'MMR012010', N'Buthidaung', N'MMR012')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (169, N'MMR012017', N'Gwa', N'MMR012')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (170, N'MMR012011', N'Kyaukpyu', N'MMR012')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (171, N'MMR012004', N'Kyauktaw', N'MMR012')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (172, N'MMR012009', N'Maungdaw', N'MMR012')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (173, N'MMR012005', N'Minbya', N'MMR012')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (174, N'MMR012003', N'Mrauk-U', N'MMR012')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (175, N'MMR012012', N'Munaung', N'MMR012')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (176, N'MMR012006', N'Myebon', N'MMR012')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (177, N'MMR012007', N'Pauktaw', N'MMR012')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (178, N'MMR012002', N'Ponnagyun', N'MMR012')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (179, N'MMR012013', N'Ramree', N'MMR012')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (180, N'MMR012008', N'Rathedaung', N'MMR012')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (181, N'MMR012001', N'Sittwe', N'MMR012')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (182, N'MMR012015', N'Thandwe', N'MMR012')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (183, N'MMR012016', N'Toungup', N'MMR012')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (184, N'MMR005014', N'Ayadaw', N'MMR005')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (185, N'MMR005023', N'Banmauk', N'MMR005')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (186, N'MMR005013', N'Budalin', N'MMR005')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (187, N'MMR005015', N'Chaung-U', N'MMR005')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (188, N'MMR005033', N'Hkamti', N'MMR005')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (189, N'MMR005034', N'Homalin', N'MMR005')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (190, N'MMR005021', N'Indaw', N'MMR005')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (191, N'MMR005027', N'Kale', N'MMR005')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (192, N'MMR005028', N'Kalewa', N'MMR005')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (193, N'MMR005007', N'Kanbalu', N'MMR005')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (194, N'MMR005017', N'Kani', N'MMR005')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (195, N'MMR005020', N'Katha', N'MMR005')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (196, N'MMR005024', N'Kawlin', N'MMR005')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (197, N'MMR005005', N'Khin-U', N'MMR005')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (198, N'MMR005008', N'Kyunhla', N'MMR005')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (199, N'MMR005036', N'Lahe', N'MMR005')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (200, N'MMR005035', N'Layshi', N'MMR005')
GO
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (201, N'MMR005031', N'Mawlaik', N'MMR005')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (202, N'MMR005029', N'Mingin', N'MMR005')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (203, N'MMR005012', N'Monywa', N'MMR005')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (204, N'MMR005003', N'Myaung', N'MMR005')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (205, N'MMR005002', N'Myinmu', N'MMR005')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (206, N'MMR005037', N'Nanyun', N'MMR005')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (207, N'MMR005019', N'Pale', N'MMR005')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (208, N'MMR005032', N'Paungbyin', N'MMR005')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (209, N'MMR005026', N'Pinlebu', N'MMR005')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (210, N'MMR005001', N'Sagaing', N'MMR005')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (211, N'MMR005018', N'Salingyi', N'MMR005')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (212, N'MMR005004', N'Shwebo', N'MMR005')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (213, N'MMR005010', N'Tabayin', N'MMR005')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (214, N'MMR005030', N'Tamu', N'MMR005')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (215, N'MMR005011', N'Taze', N'MMR005')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (216, N'MMR005022', N'Tigyaing', N'MMR005')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (217, N'MMR005006', N'Wetlet', N'MMR005')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (218, N'MMR005025', N'Wuntho', N'MMR005')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (219, N'MMR005009', N'Ye-U', N'MMR005')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (220, N'MMR005016', N'Yinmarbin', N'MMR005')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (221, N'MMR016320', N'Ho Tawng (Ho Tao)', N'MMR016')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (222, N'MMR016001', N'Kengtung', N'MMR016')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (223, N'MMR016319', N'Mong Hpen', N'MMR016')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (224, N'MMR016322', N'Mong Kar', N'MMR016')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (225, N'MMR016321', N'Mong Pawk', N'MMR016')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (226, N'MMR016010', N'Monghpyak', N'MMR016')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (227, N'MMR016006', N'Monghsat', N'MMR016')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (228, N'MMR016002', N'Mongkhet', N'MMR016')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (229, N'MMR016005', N'Mongla', N'MMR016')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (230, N'MMR016007', N'Mongping', N'MMR016')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (231, N'MMR016008', N'Mongton', N'MMR016')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (232, N'MMR016003', N'Mongyang', N'MMR016')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (233, N'MMR016011', N'Mongyawng', N'MMR016')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (234, N'MMR016323', N'Nam Hpai', N'MMR016')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (235, N'MMR016009', N'Tachileik', N'MMR016')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (236, N'MMR015311', N'Aik Chan (Ai'' Chun)', N'MMR015')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (237, N'MMR015203', N'Chinshwehaw Sub-township (Kokang SAZ)', N'MMR015')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (238, N'MMR015306', N'Hkun Mar (Hkwin Ma)', N'MMR015')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (239, N'MMR015021', N'Hopang', N'MMR015')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (240, N'MMR015305', N'Hsawng Hpa (Saun Pha)', N'MMR015')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (241, N'MMR015002', N'Hseni', N'MMR015')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (242, N'MMR015014', N'Hsipaw', N'MMR015')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (243, N'MMR015310', N'Ka Lawng Hpar', N'MMR015')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (244, N'MMR015304', N'Kawng Min Hsang', N'MMR015')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (245, N'MMR015023', N'Konkyan', N'MMR015')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (246, N'MMR015201', N'Konkyan (Kokang SAZ)', N'MMR015')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (247, N'MMR015020', N'Kunlong', N'MMR015')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (248, N'MMR015011', N'Kutkai', N'MMR015')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (249, N'MMR015012', N'Kyaukme', N'MMR015')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (250, N'MMR015001', N'Lashio', N'MMR015')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (251, N'MMR015022', N'Laukkaing', N'MMR015')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (252, N'MMR015202', N'Laukkaing (Kokang SAZ)', N'MMR015')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (253, N'MMR015309', N'Lin Haw', N'MMR015')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (254, N'MMR015307', N'Long Htan', N'MMR015')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (255, N'MMR015018', N'Mabein', N'MMR015')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (256, N'MMR015313', N'Man Man Hseng', N'MMR015')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (257, N'MMR015303', N'Man Tun', N'MMR015')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (258, N'MMR015019', N'Manton', N'MMR015')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (259, N'MMR015024', N'Matman', N'MMR015')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (260, N'MMR015008', N'Mongmao', N'MMR015')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (261, N'MMR015017', N'Mongmit', N'MMR015')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (262, N'MMR015003', N'Mongyai', N'MMR015')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (263, N'MMR015009', N'Muse', N'MMR015')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (264, N'MMR015315', N'Nam Hkam Wu', N'MMR015')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (265, N'MMR015301', N'Nam Tit', N'MMR015')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (266, N'MMR015010', N'Namhkan', N'MMR015')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (267, N'MMR015016', N'Namhsan', N'MMR015')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (268, N'MMR015015', N'Namtu', N'MMR015')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (269, N'MMR015316', N'Nar Kawng', N'MMR015')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (270, N'MMR015302', N'Nar Wee (Na Wi)', N'MMR015')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (271, N'MMR015006', N'Narphan', N'MMR015')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (272, N'MMR015314', N'Nawng Hkit', N'MMR015')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (273, N'MMR015013', N'Nawnghkio', N'MMR015')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (274, N'MMR015317', N'Pang Hkam', N'MMR015')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (275, N'MMR015318', N'Pang Yang', N'MMR015')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (276, N'MMR015005', N'Pangsang (Panghkam)', N'MMR015')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (277, N'MMR015007', N'Pangwaun', N'MMR015')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (278, N'MMR015004', N'Tangyan', N'MMR015')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (279, N'MMR015308', N'Yawng Lin', N'MMR015')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (280, N'MMR015312', N'Yin Pang', N'MMR015')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (281, N'MMR014003', N'Hopong', N'MMR014')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (282, N'MMR014004', N'Hsihseng', N'MMR014')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (283, N'MMR014005', N'Kalaw', N'MMR014')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (284, N'MMR014014', N'Kunhing', N'MMR014')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (285, N'MMR014015', N'Kyethi', N'MMR014')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (286, N'MMR014012', N'Laihka', N'MMR014')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (287, N'MMR014018', N'Langkho', N'MMR014')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (288, N'MMR014008', N'Lawksawk', N'MMR014')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (289, N'MMR014011', N'Loilen', N'MMR014')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (290, N'MMR014020', N'Mawkmai', N'MMR014')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (291, N'MMR014017', N'Monghsu', N'MMR014')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (292, N'MMR014016', N'Mongkaing', N'MMR014')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (293, N'MMR014019', N'Mongnai', N'MMR014')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (294, N'MMR014021', N'Mongpan', N'MMR014')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (295, N'MMR014013', N'Nansang', N'MMR014')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (296, N'MMR014002', N'Nyaungshwe', N'MMR014')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (297, N'MMR014010', N'Pekon', N'MMR014')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (298, N'MMR014006', N'Pindaya', N'MMR014')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (299, N'MMR014009', N'Pinlaung', N'MMR014')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (300, N'MMR014001', N'Taunggyi', N'MMR014')
GO
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (301, N'MMR014007', N'Ywangan', N'MMR014')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (302, N'MMR006010', N'Bokpyin', N'MMR006')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (303, N'MMR006001', N'Dawei', N'MMR006')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (304, N'MMR006009', N'Kawthoung', N'MMR006')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (305, N'MMR006006', N'Kyunsu', N'MMR006')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (306, N'MMR006002', N'Launglon', N'MMR006')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (307, N'MMR006005', N'Myeik', N'MMR006')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (308, N'MMR006007', N'Palaw', N'MMR006')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (309, N'MMR006008', N'Tanintharyi', N'MMR006')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (310, N'MMR006003', N'Thayetchaung', N'MMR006')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (311, N'MMR006004', N'Yebyu', N'MMR006')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (312, N'MMR013037', N'Ahlone', N'MMR013')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (313, N'MMR013044', N'Bahan', N'MMR013')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (314, N'MMR013017', N'Botahtaung', N'MMR013')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (315, N'MMR013032', N'Cocokyun', N'MMR013')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (316, N'MMR013043', N'Dagon', N'MMR013')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (317, N'MMR013020', N'Dagon Myothit (East)', N'MMR013')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (318, N'MMR013019', N'Dagon Myothit (North)', N'MMR013')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (319, N'MMR013021', N'Dagon Myothit (Seikkan)', N'MMR013')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (320, N'MMR013018', N'Dagon Myothit (South)', N'MMR013')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (321, N'MMR013030', N'Dala', N'MMR013')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (322, N'MMR013014', N'Dawbon', N'MMR013')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (323, N'MMR013040', N'Hlaing', N'MMR013')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (324, N'MMR013008', N'Hlaingtharya', N'MMR013')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (325, N'MMR013004', N'Hlegu', N'MMR013')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (326, N'MMR013003', N'Hmawbi', N'MMR013')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (327, N'MMR013006', N'Htantabin', N'MMR013')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (328, N'MMR013001', N'Insein', N'MMR013')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (329, N'MMR013041', N'Kamaryut', N'MMR013')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (330, N'MMR013028', N'Kawhmu', N'MMR013')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (331, N'MMR013026', N'Kayan', N'MMR013')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (332, N'MMR013029', N'Kungyangon', N'MMR013')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (333, N'MMR013033', N'Kyauktada', N'MMR013')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (334, N'MMR013024', N'Kyauktan', N'MMR013')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (335, N'MMR013038', N'Kyeemyindaing', N'MMR013')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (336, N'MMR013035', N'Lanmadaw', N'MMR013')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (337, N'MMR013036', N'Latha', N'MMR013')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (338, N'MMR013042', N'Mayangone', N'MMR013')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (339, N'MMR013002', N'Mingaladon', N'MMR013')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (340, N'MMR013022', N'Mingalartaungnyunt', N'MMR013')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (341, N'MMR013012', N'North Okkalapa', N'MMR013')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (342, N'MMR013034', N'Pabedan', N'MMR013')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (343, N'MMR013016', N'Pazundaung', N'MMR013')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (344, N'MMR013039', N'Sanchaung', N'MMR013')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (345, N'MMR013031', N'Seikgyikanaungto', N'MMR013')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (346, N'MMR013045', N'Seikkan', N'MMR013')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (347, N'MMR013007', N'Shwepyithar', N'MMR013')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (348, N'MMR013011', N'South Okkalapa', N'MMR013')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (349, N'MMR013005', N'Taikkyi', N'MMR013')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (350, N'MMR013015', N'Tamwe', N'MMR013')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (351, N'MMR013013', N'Thaketa', N'MMR013')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (352, N'MMR013023', N'Thanlyin', N'MMR013')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (353, N'MMR013009', N'Thingangyun', N'MMR013')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (354, N'MMR013025', N'Thongwa', N'MMR013')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (355, N'MMR013027', N'Twantay', N'MMR013')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (356, N'MMR013010', N'Yankin', N'MMR013')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (357, N'MMR013046', N'Hlaingtharya (East)', N'MMR013')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (358, N'MMR013047', N'Hlaingtharya (West)', N'MMR013')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (377, N'oho', N'jhi', N'ohjo')
INSERT [dbo].[Tbl_PlaceTownship] ([TownshipId], [TownshipCode], [TownshipName], [StateCode]) VALUES (379, N'Hello Brho', N'Hello Brho', N'Hello Brho')
SET IDENTITY_INSERT [dbo].[Tbl_PlaceTownship] OFF
GO
ALTER TABLE [dbo].[Tbl_User]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_User_Tbl_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Tbl_User] ([UserId])
GO
ALTER TABLE [dbo].[Tbl_User] CHECK CONSTRAINT [FK_Tbl_User_Tbl_User]
GO
```
