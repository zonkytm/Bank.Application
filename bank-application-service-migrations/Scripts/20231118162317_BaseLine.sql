CREATE SCHEMA if not exists banking;

comment on schema banking is 'Схема банковской базы данных';

CREATE TABLE if not exists banking."Client"
(
    id          bigserial primary key,
    login       varchar        not null unique,
    password    text           not null,
    first_name  varchar        NOT NULL,
    last_name   varchar        NOT NULL,
    middle_name varchar        null,
    salary      numeric(10, 0) NOT NULL check ( salary > 0 ),
    birthdate   date           NOT NULL
);

create index if not exists Client_login on banking."Client"(login);

-- Таблица для хранения информации о клиентах банка
comment on table banking."Client" is 'Таблица для хранения информации о клиентах банка';

-- Уникальный идентификатор клиента
comment on column banking."Client".id is 'Уникальный идентификатор клиента';

comment on column banking."Client".login is 'Логин клиента.';

comment on column banking."Client".password is 'Пароль клиента.';

-- Имя клиента
comment on column banking."Client".first_name is 'Имя клиента';

-- Фамилия клиента
comment on column banking."Client".last_name is 'Фамилия клиента';

-- Отчество клиента
comment on column banking."Client".middle_name is 'Отчество клиента';

-- Заработная плата клиента
comment on column banking."Client".salary is 'Заработная плата клиента';

-- Дата рождения клиента
comment on column banking."Client".birthdate is 'Дата рождения клиента';

CREATE TABLE if not exists banking."Bank_account"
(
    id            bigserial primary key,
    client_id     bigint references banking."Client" (id),
    account_type  varchar        NOT NULL check ( account_type != ' ' ),
    balance       numeric(10, 3) NOT NULL,
    creation_date date           NOT NULL
);

comment on column banking."Bank_account".id is 'Уникальный идентификатор счета';
comment on column banking."Bank_account".client_id is 'Ссылка на клиента, владельца счета';
comment on column banking."Bank_account".account_type is 'Тип счета (например, "Расчетный" или "Сберегательный")';
comment on column banking."Bank_account".balance is 'Баланс счета';
comment on column banking."Bank_account".creation_date is 'Дата создания счета';

CREATE TABLE if not exists banking."Card_application_status"
(
    id                 bigserial primary key,
    status_name        varchar NOT NULL,
    status_description text    NOT NULL
);

-- Таблица для хранения статусов заявок на выпуск карты
comment on table banking."Card_application_status" is 'Таблица для хранения статусов заявок на выпуск карты';

-- Уникальный идентификатор статуса заявки
comment on column banking."Card_application_status".id is 'Уникальный идентификатор статуса заявки';

-- Наименование статуса заявки
comment on column banking."Card_application_status".status_name is 'Наименование статуса заявки';

-- Описание статуса заявки
comment on column banking."Card_application_status".status_description is 'Описание статуса заявки';


CREATE TABLE if not exists banking."Card_application_type"
(
    id               bigserial primary key,
    type_name        varchar NOT NULL,
    type_description text    NOT NULL
);

-- Таблица для хранения типов заявок на выпуск карты
comment on table banking."Card_application_type" is 'Таблица для хранения типов заявок на выпуск карты';

-- Уникальный идентификатор типа заявки
comment on column banking."Card_application_type".id is 'Уникальный идентификатор типа заявки';

-- Наименование типа заявки
comment on column banking."Card_application_type".type_name is 'Наименование типа заявки';

-- Описание типа заявки
comment on column banking."Card_application_type".type_description is 'Описание типа заявки';


CREATE TABLE if not exists banking."Card_type"
(
    id                    bigserial primary key,
    card_type             varchar NOT NULL,
    card_type_description varchar NOT NULL
);

-- Таблица для хранения типов банковских карт
comment on table banking."Card_type" is 'Таблица для хранения типов банковских карт';

-- Уникальный идентификатор типа карты
comment on column banking."Card_type".id is 'Уникальный идентификатор типа карты';

-- Наименование типа карты
comment on column banking."Card_type".card_type is 'Наименование типа карты';

-- Описание типа карты
comment on column banking."Card_type".card_type_description is 'Описание типа карты';


CREATE TABLE if not exists banking."Card"
(
    id                   bigserial primary key,
    card_number          varchar NOT NULL,
    card_type_id         bigint references banking."Card_type" (id),
    card_expiration_date date    NOT NULL,
    bank_account_id      bigint references banking."Bank_account" (id)
);


comment on table banking."Card" is 'Таблица для хранения информации о банковских картах';

comment on column banking."Card".id is 'Уникальный идентификатор карты';
comment on column banking."Card".card_number is 'Номер карты';
comment on column banking."Card".card_type_id is 'Ссылка на тип карты';
comment on column banking."Card".card_expiration_date is 'Дата истечения срока действия карты';
comment on column banking."Card".bank_account_id is 'Ссылка на банковский счет, с которым связана карта';


CREATE TABLE if not exists banking."Card_application"
(
    id                         bigserial primary key,
    client_id                  bigint references banking."Client" (id),
    card_application_status_id bigint references banking."Card_application_status" (id),
    card_application_type_id   bigint references banking."Card_application_type" (id),
    open_date                  date NOT NULL,
    close_date                 date
);

-- Таблица для хранения информации о заявках на выпуск карты
comment on table banking."Card_application" is 'Таблица для хранения информации о заявках на выпуск карты';

-- Уникальный идентификатор заявки
comment on column banking."Card_application".id is 'Уникальный идентификатор заявки';

-- Ссылка на клиента, подавшего заявку
comment on column banking."Card_application".client_id is 'Ссылка на клиента, подавшего заявку';

-- Ссылка на статус заявки
comment on column banking."Card_application".card_application_status_id is 'Ссылка на статус заявки';

-- Ссылка на тип заявки
comment on column banking."Card_application".card_application_type_id is 'Ссылка на тип заявки';

-- Дата подачи заявки
comment on column banking."Card_application".open_date is 'Дата подачи заявки';

-- Дата закрытия заявки (если применимо)
comment on column banking."Card_application".close_date is 'Дата закрытия заявки (если применимо)';



CREATE TABLE if not exists banking."Client_reviews"
(
    id          bigserial primary key,
    client_id   bigint NOT NULL references banking."Client" (id),
    review_date date   NOT NULL,
    review_text varchar
);

-- Таблица для хранения отзывов клиентов
comment on table banking."Client_reviews" is 'Таблица для хранения отзывов клиентов';

-- Уникальный идентификатор отзыва
comment on column banking."Client_reviews".id is 'Уникальный идентификатор отзыва';

-- Ссылка на клиента, к которому относится отзыв
comment on column banking."Client_reviews".client_id is 'Ссылка на клиента, к которому относится отзыв';

-- Дата написания отзыва
comment on column banking."Client_reviews".review_date is 'Дата написания отзыва';

-- Текст отзыва
comment on column banking."Client_reviews".review_text is 'Текст отзыва о банке';



CREATE TABLE if not exists banking."Deposit"
(
    id              bigserial primary key,
    client_id       bigint references banking."Client" (id),
    deposit_amount  numeric(10, 3) NOT NULL,
    interest_rate   numeric(10, 3) NOT NULL,
    period_in_month integer        NOT NULL
);

-- Таблица для хранения информации о депозитах клиентов
comment on table banking."Deposit" is 'Таблица для хранения информации о депозитах клиентов';

-- Уникальный идентификатор депозита
comment on column banking."Deposit".id is 'Уникальный идентификатор депозита';

-- Ссылка на клиента, владельца депозита
comment on column banking."Deposit".client_id is 'Ссылка на клиента, владельца депозита';

-- Сумма депозита
comment on column banking."Deposit".deposit_amount is 'Сумма депозита';

-- Процентная ставка по депозиту
comment on column banking."Deposit".interest_rate is 'Процентная ставка по депозиту';

-- Срок депозита в месяцах
comment on column banking."Deposit".period_in_month is 'Срок депозита в месяцах';



CREATE TABLE if not exists banking."Deposit_history"
(
    id         bigserial primary key,
    deposit_id bigint references banking."Deposit" (id),
    open_date  date NOT NULL,
    close_date date,
    CONSTRAINT deposit_history_chk_1 CHECK ((open_date < close_date))
);

-- Таблица для хранения истории депозитов клиентов
comment on table banking."Deposit_history" is 'Таблица для хранения истории депозитов клиентов';

-- Уникальный идентификатор записи истории депозита
comment on column banking."Deposit_history".id is 'Уникальный идентификатор записи истории депозита';

-- Ссылка на депозит, к которому относится запись истории
comment on column banking."Deposit_history".deposit_id is 'Ссылка на депозит, к которому относится запись истории';

-- Дата открытия депозита
comment on column banking."Deposit_history".open_date is 'Дата открытия депозита';

-- Дата закрытия депозита
comment on column banking."Deposit_history".close_date is 'Дата закрытия депозита';

-- Проверка, что дата открытия депозита меньше даты закрытия
comment on constraint deposit_history_chk_1 on banking."Deposit_history" is 'Проверка, что дата открытия депозита меньше даты закрытия';

CREATE TABLE if not exists banking."Document_type"
(
    id               serial primary key,
    type_name        varchar NOT NULL,
    type_description text    NOT NULL
);

-- Таблица для хранения информации о типах документов
comment on table banking."Document_type" is 'Таблица для хранения информации о типах документов';

-- Уникальный идентификатор типа документа
comment on column banking."Document_type".id is 'Уникальный идентификатор типа документа';

-- Название типа документа
comment on column banking."Document_type".type_name is 'Название типа документа';

-- Описание типа документа
comment on column banking."Document_type".type_description is 'Описание типа документа';


CREATE TABLE if not exists banking."Document"
(
    id                bigserial primary key,
    client_id         bigint references banking."Client" (id),
    document_type_id  int references banking."Document_type" (id),
    document_number   varchar NOT NULL,
    issue_date        date    NOT NULL,
    expiration_date   date,
    issuing_authority varchar NOT NULL
);

-- Таблица для хранения информации о документах клиентов
comment on table banking."Document" is 'Таблица для хранения информации о документах клиентов';

-- Уникальный идентификатор документа
comment on column banking."Document".id is 'Уникальный идентификатор документа';

-- Ссылка на клиента, которому принадлежит документ
comment on column banking."Document".client_id is 'Ссылка на клиента, которому принадлежит документ';

-- Ссылка на тип документа
comment on column banking."Document".document_type_id is 'Ссылка на тип документа';

-- Номер документа
comment on column banking."Document".document_number is 'Номер документа';

-- Дата выдачи документа
comment on column banking."Document".issue_date is 'Дата выдачи документа';

-- Дата истечения срока действия документа
comment on column banking."Document".expiration_date is 'Дата истечения срока действия документа';

-- Орган, выдавший документ
comment on column banking."Document".issuing_authority is 'Орган, выдавший документ';


CREATE TABLE if not exists banking."Loan_types"
(
    id          serial primary key,
    type_name   varchar NOT NULL,
    description text    NOT NULL
);

-- Таблица для хранения типов кредитов
comment on table banking."Loan_types" is 'Таблица для хранения типов кредитов';

-- Уникальный идентификатор типа кредита
comment on column banking."Loan_types".id is 'Уникальный идентификатор типа кредита';

-- Название типа кредита
comment on column banking."Loan_types".type_name is 'Название типа кредита';

-- Описание типа кредита
comment on column banking."Loan_types".description is 'Описание типа кредита';



CREATE TABLE if not exists banking."Loan"
(
    id                 bigserial primary key,
    client_id          bigint references banking."Client" (id),
    type_id            integer references banking."Loan_types",
    interest_rate      numeric(10, 5) NOT NULL check ( interest_rate > 0 ),
    loan_amount        numeric(20, 2) not null check ( loan_amount > 0 ),
    monthly_payment    numeric(20, 2) not null check ( monthly_payment > 0 ),
    loan_term_in_month integer        NOT NULL check ( loan_term_in_month > 0 ),
    status             varchar        NOT NULL
);

-- Таблица для хранения информации о кредитах
comment on table banking."Loan" is 'Таблица для хранения информации о кредитах';

-- Уникальный идентификатор кредита
comment on column banking."Loan".id is 'Уникальный идентификатор кредита';

-- Ссылка на клиента, взявшего кредит
comment on column banking."Loan".client_id is 'Ссылка на клиента, взявшего кредит';

-- Ссылка на тип кредита
comment on column banking."Loan".type_id is 'Ссылка на тип кредита';

-- Процентная ставка по кредиту
comment on column banking."Loan".interest_rate is 'Процентная ставка по кредиту';

-- Сумма кредита
comment on column banking."Loan".loan_amount is 'Сумма кредита';

-- Ежемесячный платеж
comment on column banking."Loan".monthly_payment is 'Ежемесячный платеж';

-- Статус кредита
comment on column banking."Loan".status is 'Статус кредита';

CREATE TABLE if not exists banking."Loan_application_status"
(
    id          serial primary key,
    status_name varchar NOT NULL,
    description text    NOT NULL
);

-- Таблица для хранения статусов заявок на кредит
comment on table banking."Loan_application_status" is 'Таблица для хранения статусов заявок на кредит';

-- Уникальный идентификатор статуса заявки на кредит
comment on column banking."Loan_application_status".id is 'Уникальный идентификатор статуса заявки на кредит';

-- Наименование статуса заявки на кредит
comment on column banking."Loan_application_status".status_name is 'Наименование статуса заявки на кредит';

-- Описание статуса заявки на кредит
comment on column banking."Loan_application_status".description is 'Описание статуса заявки на кредит';


CREATE TABLE if not exists banking."Loan_application"
(
    id                 bigserial primary key,
    client_id          bigint references banking."Client" (id),
    status_id          integer references banking."Loan_application_status" (id),
    loan_amount        numeric(20, 5) NOT NULL check ( loan_amount > 0 ),
    loan_term_in_month integer        NOT NULL check ( loan_term_in_month > 0 )
);

-- Таблица для хранения информации о заявках на кредит
comment on table banking."Loan_application" is 'Таблица для хранения информации о заявках на кредит';

-- Уникальный идентификатор заявки
comment on column banking."Loan_application".id is 'Уникальный идентификатор заявки на кредит';

-- Ссылка на клиента, подавшего заявку
comment on column banking."Loan_application".client_id is 'Ссылка на клиента, подавшего заявку';

-- Ссылка на статус заявки
comment on column banking."Loan_application".status_id is 'Ссылка на статус заявки';

-- Сумма запрашиваемого кредита
comment on column banking."Loan_application".loan_amount is 'Сумма запрашиваемого кредита';

-- Срок кредита в месяцах
comment on column banking."Loan_application".loan_term_in_month is 'Срок кредита в месяцах';



CREATE TABLE if not exists banking."Loan_history"
(
    id         bigserial primary key,
    loan_id    bigint references banking."Loan" (id),
    open_date  date NOT NULL,
    close_date date,
    CONSTRAINT loan_history_chk_1 CHECK ((open_date < close_date))
);

-- Таблица для хранения истории кредитов
comment on table banking."Loan_history" is 'Таблица для хранения истории кредитов';

-- Уникальный идентификатор записи в истории кредитов
comment on column banking."Loan_history".id is 'Уникальный идентификатор записи в истории кредитов';

-- Ссылка на кредит
comment on column banking."Loan_history".loan_id is 'Ссылка на кредит';

-- Дата открытия записи в истории кредитов
comment on column banking."Loan_history".open_date is 'Дата открытия записи в истории кредитов';

-- Дата закрытия записи в истории кредитов
comment on column banking."Loan_history".close_date is 'Дата закрытия записи в истории кредитов';

-- Проверка, что дата открытия меньше даты закрытия
comment on constraint Loan_history_chk_1 on banking."Loan_history" is 'Проверка, что дата открытия записи в истории кредитов меньше даты закрытия';

-- 
-- COPY banking."Bank_account" (id, client_id, account_type, balance, creation_date) FROM stdin;
-- 1	1	Расчетный	1000.000	2023-11-12
-- 2	2	Сберегательный	5000.000	2023-11-11
-- 3	3	Расчетный	1500.000	2023-11-10
-- 4	4	Сберегательный	20000.000	2023-11-09
-- 5	5	Расчетный	3000.000	2023-11-08
-- \.
-- 
-- 
-- COPY banking."Card" (id, card_number, card_type_id, card_expiration_date, bank_account_id) FROM stdin;
-- 1	1111222233334444	1	2023-12-31	1
-- 2	2222333344445555	2	2024-06-30	2
-- 3	3333444455556666	1	2023-11-30	3
-- 4	4444555566667777	2	2024-02-28	4
-- 5	5555666677778888	1	2023-10-31	5
-- \.
-- 
-- 
-- COPY banking."Card_application" (id, client_id, card_application_status_id, card_application_type_id, open_date,
--                                  close_date) FROM stdin;
-- 12	1	1	1	2023-11-12	\N
-- 13	2	2	2	2023-11-11	\N
-- 14	3	3	3	2023-11-10	\N
-- 15	4	4	1	2023-11-09	\N
-- 16	5	5	2	2023-11-08	2023-11-29
-- \.
-- 
-- 
-- COPY banking."Card_application_status" (id, status_name, status_description) FROM stdin;
-- 1	Новая заявка	Заявка на выдачу карты только что создана и ожидает обработки.
-- 2	На рассмотрении	Заявка на выдачу карты находится в процессе рассмотрения.
-- 3	Одобрена	Заявка на выдачу карты была одобрена и ожидает выпуска.
-- 4	Отклонена	Заявка на выдачу карты была отклонена банком.
-- 5	Выдана	Карта была успешно выдана клиенту.
-- \.
-- 
-- 
-- COPY banking."Card_application_type" (id, type_name, type_description) FROM stdin;
-- 1	Выпуск карты	Заявка на первоначальное получение карты клиентом.
-- 2	Перевыпуск карты	Заявка на повторное получение карты, например, при утере или повреждении.
-- 3	Закрытие карты	Заявка на закрытие действующей карты.
-- \.
-- 
-- 
-- COPY banking."Card_type" (id, card_type, card_type_description) FROM stdin;
-- 1	Credit	Кредитная карта с возможностью отсроченного погашения средств.
-- 2	Debit	Дебетовая карта, с которой списываются средства сразу.
-- 3	Prepaid	Предоплаченная карта с фиксированным балансом.
-- 4	Business	Карта для бизнес-аккаунтов с дополнительными возможностями.
-- 5	Rewards	Карта с бонусной программой и наградами.
-- \.
-- 
-- 
-- COPY banking."Client" (id, first_name, last_name, middle_name, salary, birthdate) FROM stdin;
-- 1	Денис	Давыдов	Владимирович	60000	2003-06-01
-- 2	Анна	Иванова	Сергеевна	55000	1990-03-15
-- 3	Александр	Смирнов	Александрович	70000	1985-08-22
-- 4	Елена	Петрова	Игоревна	63000	1978-11-10
-- 5	Игорь	Козлов	Валентинович	58000	1995-05-07
-- 6	Мария	Николаева	Андреевна	62000	1982-09-29
-- 7	Михаил	Ефимов	Дмитриевич	60000	2003-07-05
-- \.
-- 
-- 
-- COPY banking."Client_reviews" (id, client_id, review_date, review_text) FROM stdin;
-- 1	1	2023-11-12	Отличный банк, всегда оперативно решают вопросы.
-- 2	2	2023-11-11	Быстрое обслуживание и дружелюбный персонал.
-- 3	3	2023-11-10	Удобные онлайн-сервисы, всегда под рукой.
-- 4	4	2023-11-09	Низкие процентные ставки по кредитам.
-- 5	5	2023-11-08	Благодарен за внимательное отношение к клиентам.
-- \.
-- 
-- 
-- COPY banking."Deposit" (id, client_id, deposit_amount, interest_rate, period_in_month) FROM stdin;
-- 1	1	10000.000	0.030	12
-- 2	2	5000.000	0.020	6
-- 3	3	20000.000	0.035	24
-- 4	4	15000.000	0.025	18
-- 5	5	30000.000	0.040	36
-- \.
-- 
-- 
-- COPY banking."Deposit_history" (id, deposit_id, open_date, close_date) FROM stdin;
-- 1	1	2023-01-01	2023-12-31
-- 2	2	2023-02-15	2023-08-15
-- 3	3	2023-03-10	2025-03-10
-- 4	4	2023-04-20	2024-10-20
-- 5	5	2023-05-05	2026-05-05
-- \.
-- 
-- 
-- COPY banking."Document" (id, client_id, document_type_id, document_number, issue_date, expiration_date,
--                          issuing_authority) FROM stdin;
-- 1	1	1	AB123456	2020-01-01	\N	МВД РФ
-- 2	1	2	123-456-789 01	2021-02-01	\N	ПФР
-- 3	2	1	CD789012	2020-03-01	\N	МВД РФ
-- 4	2	2	987-654-321 02	2021-04-01	\N	ПФР
-- 5	3	1	EF345678	2020-05-01	\N	МВД РФ
-- 6	3	2	654-321-098 03	2021-06-01	\N	ПФР
-- 7	4	1	GH567890	2020-07-01	\N	МВД РФ
-- 8	4	2	789-012-345 04	2021-08-01	\N	ПФР
-- 9	5	1	IJ678901	2020-09-01	\N	МВД РФ
-- 10	5	2	012-345-678 05	2021-10-01	\N	ПФР
-- \.
-- 
-- 
-- COPY banking."Document_type" (id, type_name, type_description) FROM stdin;
-- 1	Паспорт	Документ, удостоверяющий личность гражданина.
-- 2	СНИЛС	Страховой номер индивидуального лицевого счёта.
-- 8	Водительское удостоверение	Документ, разрешающий управление автотранспортом.
-- 9	Заграничный паспорт	Документ для поездок за границу.
-- 10	Свидетельство о рождении	Документ, удостоверяющий факт рождения.
-- \.
-- 
-- 
-- COPY banking."Loan" (id, client_id, type_id, interest_rate, loan_amount, monthly_payment, status) FROM stdin;
-- 1	1	1	0.05	10000.00	500.00	Active
-- 2	1	2	0.03	20000.00	1000.00	Active
-- 3	2	1	0.06	15000.00	750.00	Active
-- 4	2	2	0.04	25000.00	1250.00	Active
-- 5	3	1	0.07	20000.00	1000.00	Active
-- 6	3	2	0.04	30000.00	1500.00	Active
-- 7	4	1	0.08	25000.00	1250.00	Active
-- 8	4	2	0.05	35000.00	1750.00	Active
-- 9	5	1	0.09	30000.00	1500.00	Active
-- 10	5	2	0.05	40000.00	2000.00	Active
-- \.
-- 
-- 
-- COPY banking."Loan_application" (id, client_id, loan_amount, loan_term_in_month, status_id) FROM stdin;
-- 1	1	5000.00000	12	1
-- 2	2	10000.00000	24	2
-- 3	3	15000.00000	36	1
-- 4	4	20000.00000	48	2
-- 5	5	25000.00000	60	1
-- \.
-- 
-- 
-- COPY banking."Loan_application_status" (id, status_name, description) FROM stdin;
-- 1	Новая заявка	Заявка на кредит только что создана и еще не обработана.
-- 2	На рассмотрении	Заявка находится в процессе рассмотрения со стороны банка.
-- 3	Одобрена	Заявка на кредит была одобрена банком.
-- 4	Отклонена	Заявка на кредит была отклонена банком.
-- 5	Завершена	Заявка на кредит успешно завершена, кредит выдан.
-- \.
-- 
-- 
-- COPY banking."Loan_history" (id, loan_id, open_date, close_date) FROM stdin;
-- 1	1	2022-01-01	\N
-- 2	2	2022-02-01	\N
-- 3	3	2022-03-01	\N
-- 4	4	2022-04-01	\N
-- 5	5	2022-05-01	\N
-- 6	6	2022-06-01	\N
-- 7	7	2022-07-01	\N
-- 8	8	2022-08-01	\N
-- 9	9	2022-09-01	\N
-- 10	10	2022-10-01	\N
-- \.
-- 
-- 
-- COPY banking."Loan_types" (id, type_name, description) FROM stdin;
-- 1	Потребительский кредит	Кредит для личных нужд, таких как образование или медицинские расходы.
-- 2	Ипотечный кредит	Кредит на покупку или рефинансирование жилья.
-- 3	Автокредит	Кредит на покупку автомобиля, где автомобиль служит залогом.
-- 4	Бизнес-кредит	Кредит для поддержки и финансирования бизнес-деятельности.
-- 5	Студенческий кредит	Кредит, предназначенный для помощи студентам в оплате образовательных расходов.
-- \.
