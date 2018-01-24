
USE master

GO
ALTER DATABASE H2_OOP_TEC_Banking_System SET SINGLE_USER WITH ROLLBACK IMMEDIATE
GO

IF EXISTS(select * from sys.databases where name='H2_OOP_TEC_Banking_System')
DROP DATABASE H2_OOP_TEC_Banking_System
CREATE DATABASE H2_OOP_TEC_Banking_System

GO

USE H2_OOP_TEC_Banking_System


GO

CREATE TABLE Customers (
	CustomerID int IDENTITY(1,1) PRIMARY KEY,
	Created datetime NOT NULL DEFAULT GETDATE(),
	FirstName nvarchar(50) NOT NULL,
	LastName nvarchar(50) NOT NULL,
	Address nvarchar(50),
	City nvarchar(50),
	PostalCode int,
	Active bit NOT NULL default 1
	)

GO

INSERT INTO Customers (FirstName, LastName, Address, City, PostalCode)
	VALUES 	('Test', 'Testman', 'Testvej', 'TestCity', 2400),
			('Testie','Testman2', 'Testgade', 'TestVillage', 2800)
	
	
begin /* Adds 200 customers */
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Sheeree', 'Wardingley', '390 High Crossing Plaza', 'Monroe', '43');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Ernestus', 'Kaesmans', '98059 Dennis Crossing', 'Balong', '8');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Carolyne', 'Hubach', '0795 Alpine Lane', 'Chabu', '1');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Berrie', 'Gainsbury', '28 Eastwood Junction', 'Dulce Nombre de Culmí', '3');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Dosi', 'Orwell', '4 Texas Circle', 'Lenešice', '804');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Seymour', 'Buckle', '6222 Banding Avenue', 'Sueyoshichō-ninokata', '5380');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Cathie', 'Dark', '4664 Tennessee Place', 'Beixiang', '2');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Boony', 'Catto', '73624 Buena Vista Alley', 'Libertador General San Martín', '12605');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Koren', 'D''orsay', '846 Eliot Court', 'Shahrisabz Shahri', '67105');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Lurette', 'Gaylor', '61417 Shelley Parkway', 'Topol’noye', '902');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Hermann', 'McLardie', '03843 Prentice Junction', 'Kadusirung Hilir', '53815');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Bowie', 'Worboy', '47644 Comanche Street', 'Langley', '4932');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Jose', 'Colmer', '5 Raven Center', 'Hämeenkoski', '66807');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Philippine', 'Firman', '07481 Spaight Center', 'Cluses', '9195');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Lurline', 'Strettle', '0401 Kropf Center', 'Huogezhuang', '098');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Vikky', 'Tindley', '5 Raven Lane', 'Pereyaslovskaya', '2059');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Essy', 'Aizik', '97 Hagan Junction', 'Huchang', '40');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Pammy', 'Richarson', '01 Tony Crossing', 'Kafr Miṣr', '29586');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Jarid', 'Reddlesden', '62 Scott Street', 'Salinggara', '2');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Normand', 'Klamman', '09883 Continental Alley', 'Chone', '2');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Annecorinne', 'Rachuig', '8158 Banding Plaza', 'Wan’an', '70501');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Charil', 'Toopin', '0 Orin Road', 'Kiangan', '974');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Cyndi', 'Brosetti', '35845 Boyd Street', 'Pagatin', '3913');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Virge', 'Dingivan', '131 Anzinger Park', 'Tennō', '53');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Putnam', 'Rechert', '0 Clove Parkway', 'Lunéville', '1');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Tybie', 'Carbin', '30951 Eagan Circle', 'Hazlov', '6');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Janel', 'Luna', '52782 Eagan Street', 'Jajarkrajan', '913');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Jeromy', 'Somner', '0 Redwing Place', 'Union', '1532');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Minta', 'Rapinett', '6 Pearson Parkway', 'Corral de Bustos', '573');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Reinhard', 'Westwater', '033 Moland Road', 'Kekeran', '6579');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Rand', 'Philipet', '7995 Thompson Point', 'Bereeda', '311');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Benedict', 'Jeavon', '3727 Ruskin Plaza', 'Czarna Woda', '791');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Kilian', 'Dove', '5 Thackeray Junction', 'Rajūzah', '19');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Loralee', 'Conley', '65331 Parkside Crossing', 'Dhaka', '5621');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Wendy', 'Valentino', '700 Oxford Trail', 'La Aurora', '7');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Abbi', 'Fairbank', '297 Forest Run Junction', 'Kurye', '16');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Gussie', 'Swinbourne', '249 Thompson Avenue', 'Kakata', '575');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Lesley', 'Grcic', '278 Shelley Court', 'Komes', '434');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Wilhelm', 'MacRanald', '301 Vernon Parkway', 'Kladno', '9');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Terrill', 'Deppe', '4 Rieder Avenue', 'Mache', '31063');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Sybille', 'Buggy', '3987 Farragut Drive', 'Ilinden', '368');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Dorie', 'Geering', '29 Scofield Trail', 'Xylotymbou', '2');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Isacco', 'Van''t Hoff', '11254 Badeau Way', 'Danxi', '02');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Masha', 'McAllaster', '04589 Shelley Center', 'Putrajaya', '06804');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Robinett', 'Braunston', '50 Cascade Lane', 'Foúrnoi', '6');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Truda', 'Cropper', '3 Colorado Crossing', 'Karangwaru', '6202');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Sven', 'Lasty', '91293 Hayes Junction', 'Ganyi', '0681');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Gus', 'Turnell', '38 Springview Parkway', 'Reshetylivka', '8');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Mohandas', 'Lowmass', '17801 Morningstar Court', 'Jonkowo', '6100');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Chas', 'Malitrott', '15696 Larry Terrace', 'Savé', '5584');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Eddie', 'Gouinlock', '9425 Eggendart Place', 'Vryburg', '97139');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Marcia', 'Blastock', '0 Nelson Junction', 'Sotnikovskoye', '40781');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Harrietta', 'Lowis', '2 Messerschmidt Trail', 'Diourbel', '69450');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Win', 'Wallbanks', '203 Sauthoff Point', 'Thành Phố Hà Giang', '758');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Alidia', 'Malicki', '365 American Drive', 'Velykyy Burluk', '6');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Audie', 'Eddins', '2 1st Street', 'Vallauris', '369');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Thedric', 'Queyos', '126 Randy Pass', 'Bicaj', '4');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Celisse', 'Bartolomeu', '6050 Packers Way', 'Jindong', '77168');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Betteann', 'Stackbridge', '8486 Gale Drive', 'Berlin', '3961');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Friedrick', 'Tarquinio', '1 Mesta Junction', 'Wyszki', '35551');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Marlow', 'Culley', '55237 Gina Terrace', 'Navatat', '75711');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Nadya', 'Creavin', '68061 Anniversary Drive', 'Pop', '21');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Ludwig', 'Nelissen', '628 Hovde Court', 'Jamao al Norte', '63578');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Garner', 'Chapiro', '3137 Thackeray Trail', 'Sumberdadi', '03127');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Yolande', 'Goldthorpe', '93093 Kenwood Junction', 'Huangyang', '032');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Lyndsie', 'Tummons', '6 Carpenter Junction', 'Złoty Stok', '2281');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Jarid', 'Ludye', '97509 Fairfield Circle', 'Ljungby', '85174');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Carmencita', 'Boame', '7 West Park', 'Dushk', '81730');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Fredericka', 'MacAvddy', '21196 Anthes Hill', 'Kampong Cham', '08520');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Bartel', 'McNae', '11134 Kennedy Hill', 'Sịa', '29');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Gabi', 'Ferson', '01 5th Hill', 'Dzorastan', '7');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Chantalle', 'Annett', '9 Pearson Park', 'Adraskan', '0042');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Townie', 'Hub', '50792 Charing Cross Avenue', 'Hajeom', '8923');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Christian', 'Sweeten', '5 Fuller Lane', 'Hwangju-ŭp', '495');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Cloe', 'Gerhold', '95240 Springview Road', 'Sukoanyar', '965');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Dennet', 'Kondratowicz', '2 Coolidge Court', 'Triandría', '1520');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Faythe', 'Wakeman', '06 Evergreen Trail', 'Ajaccio', '4');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Winston', 'Rizzi', '93 Golf Course Court', 'Jinghai', '54');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Camey', 'Hutchason', '06026 Maple Crossing', 'Lingkou', '1266');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Ellerey', 'Glendinning', '1121 Stephen Way', 'Nova Era', '761');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Esme', 'Sherrocks', '085 Alpine Way', 'Riangbaring', '59');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Harlin', 'Dullard', '2 Westerfield Alley', 'Novyy Svit', '78');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Annalise', 'Mandel', '60 7th Point', 'Rio de Janeiro', '6378');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Ivory', 'Mulvany', '607 Knutson Way', 'Mbomba', '7230');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Sascha', 'Zimmerman', '6804 Jenifer Trail', 'Kadubincarung', '278');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Kamillah', 'Domange', '21136 Park Meadow Park', 'Meringkik', '25');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Paige', 'Tombleson', '32 Fulton Pass', 'Sao Hai', '2200');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Barb', 'Gaylard', '9486 Oneill Pass', 'Montauban', '746');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Rowen', 'De Witt', '87 Knutson Trail', 'Pamiers', '452');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Mervin', 'de Keyser', '28 Algoma Junction', 'Baiyushan', '8');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Marsiella', 'Linscott', '4 Lyons Junction', 'Newark', '1');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Jaymee', 'Sabate', '3 Thompson Parkway', 'Callahuanca', '705');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Coop', 'Flello', '92297 Marquette Road', 'Longcang', '635');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Therese', 'Abbett', '00205 Arkansas Terrace', 'Radomir', '3');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Aubrette', 'Roscoe', '3 Stephen Point', 'Tân Phú', '0658');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Syd', 'Blabie', '81 Susan Road', 'Tieba', '847');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Grant', 'Moreinis', '98216 Carberry Junction', 'Shangma', '565');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Ashia', 'Mangin', '4508 Daystar Street', 'Mache', '91317');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Bria', 'Blinder', '0200 Heath Parkway', 'Saint Joseph', '0538');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Wanids', 'Lilbourne', '1 Mayfield Hill', 'Velykyy Burluk', '791');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Horst', 'O'' Dornan', '20137 Dahle Pass', 'Sanshui', '05524');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Husein', 'Sleit', '89854 Maywood Park', 'Bacnar', '8793');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Hayes', 'Whatham', '9 Sugar Pass', 'Maksatikha', '667');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Lydie', 'Labba', '41 Sunnyside Junction', 'Taguisa', '61861');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Shae', 'MacMenamy', '65 Norway Maple Hill', 'Jatisari', '94');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Casandra', 'Cuddehay', '57959 Shoshone Avenue', 'Santander', '341');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Manda', 'Couper', '1991 Pond Avenue', 'Kineshma', '02854');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Eddie', 'Peyes', '2705 Kipling Street', 'Moutfort', '5963');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Issy', 'Meffen', '0 Debs Court', 'Yangying', '157');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Hercule', 'Shine', '5 Porter Circle', 'Yangi-Nishon Shahri', '8');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Lainey', 'McAvaddy', '82986 Donald Circle', 'Viana do Alentejo', '749');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Maurizia', 'Napolione', '67731 Sycamore Street', 'Tanūmah', '91699');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Adan', 'Warrilow', '2 Marcy Park', 'Amos', '19913');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Fiorenze', 'Hardware', '92 Hansons Street', 'Rauco', '6152');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Justino', 'Davson', '66179 Fair Oaks Trail', 'Xincheng', '3033');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Cosette', 'Taphouse', '0 Cordelia Road', 'San Antonio', '3');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Enos', 'Mandrake', '615 Elgar Place', 'Dukuhbadag', '2603');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Sayres', 'Santori', '10 High Crossing Crossing', 'Kolirerek', '74');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Chrotoem', 'Khristyukhin', '61165 Armistice Circle', 'Guyancourt', '7816');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Terrel', 'Swales', '66 Golf Terrace', 'Datong', '8618');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Wenona', 'Normanton', '098 Hayes Junction', 'Ust’-Donetskiy', '071');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Maxine', 'Yeude', '77 Sachs Lane', 'Primero de Enero', '6978');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Clemmie', 'Bassam', '313 Arrowood Avenue', 'Azenhas do Mar', '2');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Maurits', 'Mumbeson', '62 Cascade Pass', 'Stockholm', '10');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Nickolai', 'Rigard', '5 Reindahl Way', 'Shibajiazi', '4471');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Marji', 'Lambillion', '7375 Thierer Point', 'Baliuag Nuevo', '8777');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Lauri', 'Royson', '4 Bowman Alley', 'København', '45997');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Revkah', 'Fley', '4503 Donald Street', 'Nkoteng', '6047');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Caty', 'Bernath', '5963 Paget Drive', 'Calaba', '0');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Ted', 'Greatland', '4 Veith Terrace', 'Tanay', '35');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Whitney', 'John', '8873 Hoepker Drive', 'Nikopol’', '3104');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Gaultiero', 'Lavington', '3189 Carey Circle', 'Ban Tak', '47');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Saunder', 'Domelaw', '174 Erie Junction', 'Tuanai', '15521');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Lavina', 'Widmore', '7485 Delaware Place', 'Nova Kakhovka', '43078');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Lynne', 'Ranfield', '1 Northridge Drive', 'Majunying', '3');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Laverna', 'Bullivant', '3 Reinke Parkway', 'Shicheng', '05');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Barbra', 'Abramow', '224 Northport Pass', 'Yanwang', '7');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Lishe', 'Klimp', '9838 Bultman Pass', 'Banī Zayd', '51349');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Tomi', 'Trowl', '62929 Monica Circle', 'Bjärred', '986');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Rica', 'Pettett', '39 Nancy Road', 'Niort', '3521');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Sampson', 'Bichard', '8918 Myrtle Park', 'Wailebe', '10641');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Nisse', 'Belt', '10797 American Ash Court', 'Stratford', '24');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Ainslee', 'Jocic', '805 Armistice Parkway', 'Austin', '04');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Biron', 'Pattisson', '07306 Gateway Pass', 'Bealanana', '5');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Kirsteni', 'Laphorn', '31 Fairfield Junction', 'Xinmin', '72');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Hyacinthie', 'Just', '7395 Corben Point', 'El Copey', '2');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Leilah', 'Seccombe', '1389 Arkansas Way', 'Słupsk', '59827');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Federica', 'Espinel', '3 Susan Pass', 'Sestroretsk', '78375');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Gelya', 'Dransfield', '89815 Forest Park', 'Guanzhou', '7');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Marlow', 'Jozaitis', '64842 Kim Crossing', 'Iecava', '2903');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Cammie', 'Wiburn', '906 Northridge Avenue', 'New York City', '86061');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Shep', 'Bodley', '5 Marquette Avenue', 'Timbó', '187');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Goldy', 'Moorton', '02 Green Ridge Trail', 'Inawashiro', '663');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Etienne', 'Gourdon', '83355 Hoffman Circle', 'Malysheva', '561');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Tillie', 'Tilzey', '546 Glacier Hill Street', 'Lomboy', '55652');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Mahmud', 'McHan', '5 Melby Place', 'Tambaksari', '69');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Sharleen', 'Amorts', '92939 Monica Road', 'Reshetnikovo', '9');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Giordano', 'Kern', '0 Derek Parkway', 'Erdu', '961');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Fransisco', 'Camillo', '3 Harper Street', 'Curibaya', '306');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Cairistiona', 'Osinin', '0535 Oneill Pass', 'Lodoyo', '78');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Sibyl', 'Blankett', '632 Acker Trail', 'Sujitan', '1');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Sharl', 'Leedal', '8 International Point', 'Pila', '4');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Marline', 'Pedrielli', '740 Thackeray Avenue', 'Mambulo', '723');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Diane-marie', 'Pigne', '57 Nova Street', 'Andorinha', '5');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Noak', 'Simoni', '7 Northfield Avenue', 'Naikoten Dua', '70');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Verne', 'Cowell', '84 Stephen Park', 'Sumberpandan', '97975');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Florina', 'Hackwell', '634 Oneill Avenue', 'Baiyin', '1');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Mara', 'Rookwell', '11980 International Crossing', 'Sutukoba', '75387');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Boone', 'Wallas', '1 Pawling Court', 'Yegor’yevsk', '7569');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Agosto', 'Sivills', '91 Arrowood Terrace', 'Manning', '6');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Kris', 'Kops', '0 Jana Junction', 'Yiánnouli', '7485');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Bogart', 'Prandy', '4532 Bobwhite Parkway', 'Jianling', '2184');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Zulema', 'Cunnell', '56961 Goodland Drive', 'Astara', '0145');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Eadmund', 'Reddan', '8 Bluestem Center', 'Parajara', '2');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Mohandis', 'Pyett', '341 Old Gate Lane', 'San Antonio Ilotenango', '64161');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Ozzie', 'Mangeon', '37 Westport Junction', 'Kamennyye Potoki', '65460');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Cyrille', 'Luby', '27 Basil Drive', 'Akwanga', '6');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Kamila', 'Snasel', '0303 Prairieview Pass', 'Tarimbang', '2');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Upton', 'Challener', '0 Atwood Lane', 'Coelho Neto', '06750');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Shaylynn', 'Lummus', '7881 Prairie Rose Avenue', 'Arnelas', '00');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Romola', 'Mollindinia', '511 Express Parkway', 'Wuyang', '2973');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Aurore', 'Balmadier', '71523 Meadow Ridge Circle', 'Shuangxikou', '3402');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Lilah', 'Ronchka', '1 Thackeray Drive', 'Hengjing', '42626');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Allsun', 'Jupp', '460 Kennedy Trail', 'Kelin', '8500');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Billye', 'Robb', '81002 Ruskin Trail', 'Victoria', '269');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Alphonso', 'Spaven', '5 Reinke Place', 'Tagoloan', '87763');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Barrie', 'Gwyer', '01 Blaine Trail', 'Si Somdet', '6029');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Delmor', 'Pontin', '49 Heffernan Alley', 'San Antonio de los Baños', '641');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Liesa', 'Gerran', '2421 Bunting Junction', 'Boca do Acre', '3174');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Bradney', 'Ingleson', '46280 Brickson Park Way', 'Sacramento', '07');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Brendon', 'Guinery', '7839 Atwood Park', 'Mengeš', '78610');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Lionel', 'Oglevie', '097 Hanson Drive', 'Tanjung Pandan', '86015');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Laurie', 'Traise', '302 Rusk Street', 'Tando Muhammad Khān', '1425');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Lawry', 'Kestian', '05 Reinke Terrace', 'Barengkok', '1');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Monti', 'Cunniff', '35 Carioca Center', 'Calhetas', '481');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Elinore', 'Cruikshank', '3260 Sachtjen Crossing', 'La‘l', '9');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Irvin', 'Newell', '56 Karstens Pass', 'Rancabungur', '0');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Ulrick', 'Stute', '32 Everett Crossing', 'Foros de Salvaterra', '5');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Chadwick', 'Dahmke', '2804 Esker Way', 'San Diego', '911');
insert into Customers (Firstname, lastname, address, city, postalcode) values ('Hanny', 'Killigrew', '675 Manitowish Center', 'Bulu', '5');

end
	
Create Table AccountTypes (
	AccountTypeID int IDENTITY(1,1) PRIMARY KEY,
	AccountTypeName nvarchar(50),
	InterestRate float NOT NULL default 0
)

GO

INSERT INTO ACcountTypes (AccountTypeName, InterestRate)
VALUES	('Opsparing', 0.027), 
		('Pensionskonto', 0.04), 
		('Børneopsparing', 0.05), 
		('BudgetKonto', 0.01),
		('Børnekonto', 0.008)
		
INSERT INTO ACcountTypes (AccountTypeName)
VALUES	('Lånekonto'),
		('Lønkonto')

Create Table Accounts (
	AccountID int IDENTITY(1,1) PRIMARY KEY,
	CustomerId int NOT NULL FOREIGN KEY REFERENCES Customers(CustomerID),
	Created datetime DEFAULT GETDATE(),
	AccountNo int NOT NULL UNIQUE,
	AccountTypeId int FOREIGN KEY REFERENCES AccountTypes(AccountTypeID),
	Saldo float default 0,
	Active bit NOT NULL default 1,
	)

GO

INSERT INTO Accounts (customerId, AccountNo, AccountTypeId, saldo)
VALUES	(1, 1001, 3, 10000),
		(2, 1002, 2, -3.50),
		(2, 1003, 6, 1337.65)

begin /* Adds 100 accounts */
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (1, 1004, 6, 33451.26);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (2, 1005, 6, 658181.48);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (3, 1006, 6, 496828.25);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (4, 1007, 6, 226446.75);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (5, 1008, 6, 934678.74);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (6, 1009, 6, 165264.94);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (7, 1010, 6, 480425.93);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (8, 1011, 5, 197483.43);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (9, 1012, 5, 7758.35);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (10, 1013, 5, 374177.25);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (11, 1014, 5, 108246.77);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (12, 1015, 5, 872806.23);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (13, 1016, 5, 505645.19);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (14, 1017, 5, 838944.52);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (15, 1018, 5, 353490.22);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (16, 1019, 5, 234931.08);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (17, 1020, 5, 842466.1);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (18, 1021, 5, 398969.65);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (19, 1022, 5, 720774.0);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (20, 1023, 5, 830264.57);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (21, 1024, 4, 187733.04);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (22, 1025, 4, 97674.5);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (23, 1026, 4, 593018.51);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (24, 1027, 4, 932726.71);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (25, 1028, 4, 229678.89);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (26, 1029, 4, 223798.45);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (27, 1030, 4, 893268.47);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (28, 1031, 4, 71174.45);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (29, 1032, 4, 882084.15);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (30, 1033, 4, 444735.1);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (31, 1034, 4, 316475.33);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (32, 1035, 4, 60208.42);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (33, 1036, 4, 182570.71);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (34, 1037, 4, 429922.22);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (35, 1038, 4, 116006.93);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (36, 1039, 4, 989125.24);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (37, 1040, 4, 983333.3);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (38, 1041, 4, 575378.62);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (39, 1042, 4, 590672.1);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (40, 1043, 4, 144040.74);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (41, 1044, 3, 216016.28);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (42, 1045, 3, 901004.78);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (43, 1046, 3, 730299.96);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (44, 1047, 3, 853565.81);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (45, 1048, 3, 831105.9);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (46, 1049, 3, 947613.79);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (47, 1050, 3, 668297.26);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (48, 1051, 3, 574639.56);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (49, 1052, 3, 436518.86);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (50, 1053, 3, 106922.75);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (51, 1054, 3, 36318.24);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (52, 1055, 3, 981726.0);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (53, 1056, 3, 544310.44);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (54, 1057, 3, 82962.67);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (55, 1058, 3, 811130.05);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (56, 1059, 3, 504493.11);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (57, 1060, 3, 547015.84);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (58, 1061, 3, 954868.0);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (59, 1062, 3, 386200.45);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (60, 1063, 3, 181139.44);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (61, 1064, 2, 279034.92);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (62, 1065, 2, 525094.54);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (63, 1066, 2, 474634.71);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (64, 1067, 2, 48091.88);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (65, 1068, 2, 109408.25);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (66, 1069, 2, 392611.99);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (67, 1070, 2, 212371.53);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (68, 1071, 2, 639493.9);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (69, 1072, 2, 333629.37);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (70, 1073, 2, 573485.98);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (71, 1074, 2, 607069.4);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (72, 1075, 2, 450395.16);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (73, 1076, 2, 723208.78);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (74, 1077, 2, 394245.47);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (75, 1078, 2, 747847.0);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (76, 1079, 2, 691189.39);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (77, 1080, 2, 502387.53);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (78, 1081, 2, 487113.17);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (79, 1082, 2, 452747.36);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (80, 1083, 2, 628009.98);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (81, 1084, 1, 587237.04);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (82, 1085, 1, 148171.32);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (83, 1086, 1, 845031.96);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (84, 1087, 1, 555556.0);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (85, 1088, 1, 372949.3);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (86, 1089, 1, 519492.45);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (87, 1090, 1, 364089.57);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (88, 1091, 1, 61503.98);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (89, 1092, 1, 370683.21);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (90, 1093, 1, 563674.22);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (91, 1094, 1, 214246.91);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (92, 1095, 1, 226659.6);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (93, 1096, 1, 317891.82);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (94, 1097, 1, 297256.03);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (95, 1098, 1, 441726.21);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (96, 1099, 1, 709403.69);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (97, 1100, 1, 344300.3);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (98, 1101, 1, 75410.95);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (99, 1102, 1, 551855.69);
insert into Accounts (customerid, accountNo, accounttypeId, saldo) values (100, 1103, 1, 185771.86);

end		
		
CREATE TABLE TransactionTypes (
	TransactionTypeID int PRIMARY KEY NOT NULL ,
	TransactionName nvarchar(50) NOT NULL
	)

INSERT INTO TransactionTypes(TransactionTypeID,TransactionName)
VALUES 
(1, 'Udbetaling'),
(2, 'Indbetaling')

GO

CREATE TABLE Transactions (
	TransactionID int IDENTITY(1,1) PRIMARY KEY,
	AccountId int NOT NULL FOREIGN KEY REFERENCES Accounts(AccountID),
	Created datetime NOT NULL default GETDATE(),
	Amount float NOT NULL,
	TransactionTypeId int NOT NULL FOREIGN KEY REFERENCES TransactionTypes(TransactionTypeID)
	)

GO

INSERT INTO Transactions (AccountId, Amount, TransactionTypeID)
VALUES		(1, 200, 2),
			(1, -300, 1),
			(3, 600, 2),
			(2, -10, 1)

GO

begin
insert into Transactions (AccountId, Amount, transactiontypeid) values (1, 24414.67, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (2, 31393.61, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (3, 42456.86, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (4, 10742.89, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (5, 49961.74, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (6, 29528.62, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (7, 21253.2, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (8, 45848.9, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (9, 45045.43, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (10, 36484.63, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (11, 7321.84, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (12, 11429.62, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (13, 13317.67, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (14, 29636.47, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (15, 39110.86, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (16, 17878.52, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (17, 16281.13, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (18, 15715.6, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (19, 41100.7, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (20, 6586.08, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (21, 18780.77, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (22, 25973.11, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (23, 4501.99, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (24, 5375.11, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (25, 22475.64, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (26, 11341.83, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (27, 41585.73, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (28, 19785.05, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (29, 40464.46, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (30, 15942.51, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (31, 47238.09, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (32, 34669.12, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (33, 8921.38, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (34, 11192.76, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (35, 27320.6, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (36, 16962.73, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (37, 5467.47, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (38, 22153.51, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (39, 24574.2, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (40, 40714.63, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (41, 4043.75, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (42, 26745.25, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (43, 43360.18, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (44, 4274.09, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (45, 33314.44, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (46, 36287.41, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (47, 38004.68, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (48, 40560.07, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (49, 3104.01, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (50, 21582.35, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (51, 48185.82, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (52, 8124.27, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (53, 4442.15, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (54, 21743.04, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (55, 11026.75, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (56, 40729.45, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (57, 50674.31, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (58, 9514.77, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (59, 40313.34, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (60, 31245.17, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (61, 33654.85, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (62, 5142.38, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (63, 33662.29, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (64, 41391.59, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (65, 46838.21, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (66, 33582.95, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (67, 19824.62, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (68, 28285.98, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (69, 50043.55, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (70, 1285.11, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (71, 29096.56, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (72, 3345.57, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (73, 23971.57, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (74, 49693.06, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (75, 14883.67, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (76, 21103.33, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (77, 25786.58, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (78, 33474.24, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (79, 40999.49, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (80, 21138.0, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (81, 41284.11, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (82, 26046.92, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (83, 8497.19, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (84, 2779.33, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (85, 34147.13, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (86, 34815.17, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (87, 17759.45, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (88, 27724.07, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (89, 30557.45, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (90, 40683.77, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (91, 2259.2, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (92, 40925.17, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (93, 44932.59, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (94, 31673.59, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (95, 13446.06, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (96, 18778.11, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (97, 15068.5, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (98, 30491.58, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (99, 18854.98, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (100, 30018.56, 2);
insert into Transactions (AccountId, Amount, transactiontypeid) values (1, -3176.54, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (2, -19917.62, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (3, -14625.34, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (4, -12442.54, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (5, -48147.25, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (6, -43112.19, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (7, -16064.61, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (8, -23793.27, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (9, -42580.71, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (10, -22255.15, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (11, -23324.58, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (12, -25704.82, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (13, -3489.19, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (14, -35042.8, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (15, -43208.14, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (16, -46878.44, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (17, -29362.86, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (18, -26887.3, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (19, -1029.09, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (20, -13166.54, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (21, -40993.89, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (22, -23268.8, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (23, -13796.12, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (24, -20473.47, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (25, -1887.08, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (26, -18860.92, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (27, -18176.6, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (28, -8911.66, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (29, -8574.14, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (30, -9553.47, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (31, -16245.46, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (32, -25180.93, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (33, -36014.64, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (34, -26120.84, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (35, -3981.23, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (36, -35280.6, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (37, -18119.98, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (38, -575.79, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (39, -12663.38, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (40, -44848.42, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (41, -34006.62, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (42, -21020.27, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (43, -44165.8, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (44, -6655.51, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (45, -28511.04, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (46, -16303.71, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (47, -21766.77, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (48, -30110.91, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (49, -47153.29, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (50, -30867.42, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (51, -48022.16, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (52, -46334.8, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (53, -10377.75, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (54, -19410.37, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (55, -31323.98, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (56, -31628.19, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (57, -36473.74, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (58, -1508.92, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (59, -5174.77, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (60, -40012.45, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (61, -9938.0, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (62, -26400.13, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (63, -33756.51, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (64, 572.57, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (65, -41832.25, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (66, -48439.1, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (67, -34654.68, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (68, -34514.32, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (69, -15378.04, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (70, -31454.77, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (71, -40415.46, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (72, -9748.69, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (73, -6182.39, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (74, -7806.93, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (75, -14711.08, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (76, -1664.42, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (77, -29608.35, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (78, -11604.54, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (79, -2493.08, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (80, -470.13, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (81, -41781.5, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (82, -3308.95, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (83, -22694.16, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (84, -40361.97, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (85, -7621.04, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (86, -14888.31, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (87, -28978.04, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (88, -39453.9, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (89, -29476.24, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (90, -42292.45, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (91, -9066.7, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (92, -1733.67, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (93, -21719.57, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (94, -15474.57, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (95, -41706.43, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (96, -16948.96, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (97, -8466.67, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (98, -47071.65, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (99, -46002.73, 1);
insert into Transactions (AccountId, Amount, transactiontypeid) values (100, -8751.93, 1);

end


CREATE TABLE Users (
	UserID int IDENTITY(1,1) PRIMARY KEY,
	Created DATETIME default GETDATE(),
	UserName nvarchar(50) NOT NULL,
	UserPassword nvarchar(32) NOT NULL
	)
GO

INSERT INTO Users (UserName, UserPassword)
VALUES ('bruger', '5f4dcc3b5aa765d61d8327deb882cf99')
	   


CREATE TABLE UserLogging (
	LoggingID int IDENTITY(1,1) PRIMARY KEY,
	UserId int NOT NULL FOREIGN KEY REFERENCES Users(UserID),
	Timestamp datetime default GETDATE()
	)
GO

INSERT INTO USerLogging (UserId)
VALUES (1)
		
	
INSERT INTO USerLogging (UserId,timestamp)
VALUES (1, '2018-01-20'),
	   (1, '2018-01-20')
	   
		
	

/*SELECT CONCAT(FirstName, ' ', Lastname) as Name,
Active,
AccountTypes.AccountTypeName
FROM customers
INNER JOIN Accounts ON Customers.CustomerID = Accounts.CustomerId
INNER JOIN AccountTypes ON Accounts.AccountTypeId = AccountTypes.AccountTypeID



SELECT AccountNo, AccountTypeName, Transactions.Created, Transactions.Amount, TransactionName
FROM customers
JOIN Accounts ON Customers.CustomerID = Accounts.CustomerId
JOIN AccountTypes ON Accounts.AccountTypeId = AccountTypes.AccountTypeID
JOIN Transactions ON Accounts.AccountID = Transactions.AccountId
JOIN TransactionTypes ON Transactions.TransactiontypeId = TransactionTypes.TransactionTypeID
WHERE Customers.customerid = 1


SELECT Transactions.TransactionID, Accounts.AccountNo, Transactions.Created, Transactions.Amount, TransactionTypes.TransactionName
FROM Transactions
JOIN Accounts ON Accounts.AccountID = Transactions.AccountId
JOIN TransactionTypes ON Transactions.TransactionTypeId = TransactionTypes.TransactionTypeID
WHERE AccountNO = 4250

SELECT AccountID, Accounts.CustomerId, Accounts.Created, AccountNo, Accounts.AccountTypeId, Saldo, Accounts.Active FROM Accounts 
INNER JOIN AccountTypes ON accounts.AccountTypeId = accounttypes.AccountTypeID
INNER JOIN customers ON accounts.customerid = Customers.CustomerID
WHERE CONCAT(firstname, ' ',lastname) like '%%'

DELETE FROM Transactions
WHERE AccountID IN(SELECT DISTINCT AccountID FROM Accounts WHERE accounts.CustomerId=2); 
DELETE FROM Accounts WHERE Accounts.customerId = 2;
DELETE FROM Customers where CustomerID = 2;

SELECT COUNT(*) FROM Customers where CustomerID = @custid

cmd cmd.Scalar != 1
*/
