create table redirection
(
	Id int auto_increment
		primary key,
	Path varchar(1000) not null,
	RedirectUrl varchar(2000) not null,
	HitCount int default 0 not null,
	Created datetime not null,
	LastHit datetime null,
	IsPermanent bit default b'0' null,
	Checked bit default b'0' null,
	Return404 bit default b'0' not null,
	constraint Redirection_Path_uindex
		unique (Path)
);

