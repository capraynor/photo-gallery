--SQLite

CREATE TABLE Photos (
    Id                CHAR (36)      PRIMARY KEY
                                     NOT NULL
                                     UNIQUE,
    MD5Str            CHAR (32)      UNIQUE,
    Latitude          DOUBLE,
    Longitude         DOUBLE,
    ShottingDate      DATE,
    CreatedDate       DATE,
    FilePath          VARCHAR (1000) NOT NULL,
    ThumbnailFilePath VARCHAR (1000) 
);
