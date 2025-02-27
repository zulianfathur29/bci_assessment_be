create table users (
	id serial not null,
	username varchar not null,
	"password" varchar not null,
	last_login timestamp without time zone DEFAULT (now() at time zone 'utc'),
	constraint users_pk primary key (id),
	constraint users_unique unique (username)
);


create table project (
	id varchar(6) primary key not null,
	name varchar(200) not null,
	stage varchar not null,
	category varchar not null,
	others varchar null,
	start_date date not null,
	description varchar(2000) not null,
	created_by integer not null,
	created_at timestamp without time zone default (now() at time zone 'utc'),
	last_updated_by integer not null,
	last_updated_at timestamp without time zone default (now() at time zone 'utc')
);


CREATE OR REPLACE FUNCTION before_project_insert()
RETURNS trigger AS
$BODY$
BEGIN
   select 
        lpad(cast(coalesce(max(cast(id AS integer)), 0) + 1 as varchar), 6, '0')
        into NEW.id
    from project; 

  RETURN NEW;
END;
$BODY$
LANGUAGE plpgsql;

create or replace TRIGGER before_project_insert
 BEFORE INSERT ON project
 FOR EACH ROW 
 EXECUTE PROCEDURE before_project_insert();
 
 
 INSERT INTO project (username, password) VALUES ('zulian', 'RPRna//HxDJiKRg8tOifkNh6EJNdhXXRwmEX0e2uTcY=');