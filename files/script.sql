CREATE TABLE "User" (
    Id SERIAL PRIMARY KEY,
    Name VARCHAR(200) NOT NULL,
    LastName VARCHAR(200) NOT NULL,
    Email VARCHAR(200) NOT NULL,
    Password VARCHAR(200) NOT NULL,
    Photo VARCHAR(500),
    CreatedAt TIMESTAMP NOT NULL,
    UpdatedAt TIMESTAMP
);

CREATE TABLE "Post" (
    Id SERIAL PRIMARY KEY,
    Text VARCHAR(200) NOT NULL,
    Photo VARCHAR(500) NOT NULL,
    CreatedAt TIMESTAMP NOT NULL,
    UpdatedAt TIMESTAMP,
    UserId INT,
    CONSTRAINT FK_POST_USER FOREIGN KEY (UserId) REFERENCES "User"(Id)
);

INSERT INTO "User" (Name, LastName, Email, Password, Photo, CreatedAt, UpdatedAt) VALUES
('Janis', 'Joplin', 'janis@mail.com', '46f94c8de14fb36680850768ff1b7f2a', '4b0a5bindex.jpg', '2021-04-18 14:49:16.698882', '2021-04-26 14:49:26.435425'),
('Jimi', 'Hendrix', 'jimi@mail.com', '46f94c8de14fb36680850768ff1b7f2a', '4cc947jimiprofile.jpg', '2021-04-20 14:29:16.749603', '2021-04-26 14:45:12.258211'),
('Bob', 'Marley', 'bob@mail.com', '46f94c8de14fb36680850768ff1b7f2a', '46871abobprofile.jpg', '2021-04-21 14:46:00.556941', '2021-04-26 14:11:48.999775');

INSERT INTO "Post" (Text, Photo, CreatedAt, UpdatedAt, UserId) VALUES
('Everything that you want from me negative will stick to your chest and return in the form of peace.', 'cf96c2bobpost2.jpg', '2021-04-22 14:54:59.629374', null, 3),
('The only thing you have in this life that is really worth it are feelings.', 'ada3a2janispost2.jpg', '2021-04-23 14:30:37.773427', null, 1),
('To change the world you need to change your mind.', '61615bjimipost3.jpg', '2021-04-23 14:31:53.475346', null, 2),
('As long as eye color is more important than skin color, there will be war!', 'ac6612bobpost1.jpg', '2021-04-23 14:51:58.845436', null, 3),
('When the power of love overcomes the love of power the world will know peace.', '1f3fe2jimipost2.jpg', '2021-04-25 14:46:10.952131', null, 2),
('If those who dont like me knew what I feel for them, they would like it even less.', '3136f6bobpost3.jpg', '2021-04-26 14:40:48.199879', null, 3),
('It is better to live, Ten years of an effervescent life than to die at seventy and have spent your life watching TV.', 'f0e194janispost1.jpg', '2021-04-26 14:47:37.880515', null, 1);