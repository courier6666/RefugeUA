import { VolunteerGroup } from "../../../core/models"

export const mockVolunteerGroups: VolunteerGroup[] = [
  {
    id: 1,
    title: "Підтримка ветеранів АТО",
    descriptionContent: "Забезпечення психологічної підтримки та юридичної допомоги ветеранам.",
    createdAt: new Date("2024-12-01"),
    followers: [
      {
        id: 13,
        firstName: 'Сергій',
        lastName: 'Петренко',
        email: 'serhiy.petrenko@example.com',
        phoneNumber: '+380991234567',
        dateOfBirth: new Date('1990-03-15'),
        createdAt: new Date('2023-05-25')
      }
    ],
    admins: [
      {
        id: 14,
        firstName: 'Оксана',
        lastName: 'Мельник',
        email: 'oksana.melnyk@example.com',
        phoneNumber: '+380991112233',
        dateOfBirth: new Date('1987-06-02'),
        createdAt: new Date('2022-10-01')
      }
    ],
  },
  {
    id: 2,
    title: "Гуманітарна допомога фронту",
    descriptionContent: "Організовуємо передачу продуктів, одягу, генераторів для ЗСУ.",
    createdAt: new Date("2025-01-15"),
    followers: [],
    admins: [
      {
        id: 15,
        firstName: 'Іван',
        lastName: 'Коваль',
        email: 'ivan.koval@example.com',
        phoneNumber: '+380991122334',
        dateOfBirth: new Date('1992-09-20'),
        createdAt: new Date('2023-06-30')
      }
    ],

  },
  {
    id: 3,
    title: "Медична допомога військовим",
    descriptionContent: "Волонтери допомагають у шпиталях з доглядом за пораненими.",
    createdAt: new Date("2024-11-20"),
    followers: [
      {
        id: 16,
        firstName: 'Наталія',
        lastName: 'Шевченко',
        email: 'natalia.shevchenko@example.com',
        phoneNumber: '+380501234567',
        dateOfBirth: new Date('1985-04-10'),
        createdAt: new Date('2022-08-11')
      }
    ],
    admins: [
      {
        id: 17,
        firstName: 'Андрій',
        lastName: 'Бондар',
        email: 'andrii.bondar@example.com',
        phoneNumber: '+380991223344',
        dateOfBirth: new Date('1989-12-01'),
        createdAt: new Date('2021-07-14')
      }
    ],
  },
  {
    id: 4,
    title: "Курси перекваліфікації ветеранів",
    descriptionContent: "Допомагаємо ветеранам знайти роботу та здобути нову професію.",
    createdAt: new Date("2025-02-01"),
    followers: [],
    admins: [
      {
        id: 18,
        firstName: 'Марія',
        lastName: 'Захарова',
        email: 'maria.zakharova@example.com',
        phoneNumber: '+380971234567',
        dateOfBirth: new Date('1993-03-22'),
        createdAt: new Date('2023-01-12')
      }
    ],
  },
  {
    id: 5,
    title: "Підтримка сімей загиблих військових",
    descriptionContent: "Психологічна підтримка та матеріальна допомога родинам героїв.",
    createdAt: new Date("2025-03-10"),
    followers: [],
    admins: [
      {
        id: 19,
        firstName: 'Роман',
        lastName: 'Лисенко',
        email: 'roman.lysenko@example.com',
        phoneNumber: '+380931234567',
        dateOfBirth: new Date('1988-07-19'),
        createdAt: new Date('2022-12-30')
      }
    ],
  }
]
