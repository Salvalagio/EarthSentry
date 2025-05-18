create table earth.tb_users (
    userid serial primary key,
    username varchar(50) not null unique,
    email varchar(100) not null unique,
    passwordhash varchar(255) not null,
    imageurl TEXT NULL,
    createdat timestamp default current_timestamp,
    lastlogin timestamp,
    isactive boolean default true
);

create table earth.tb_roles (
    roleid serial primary key,
    rolename varchar(50) not null unique
);

create table earth.tb_userroles (
    userroleid serial primary key,
    userid int not null references tb_users(userid) on delete cascade,
    roleid int not null references tb_roles(roleid) on delete cascade
);