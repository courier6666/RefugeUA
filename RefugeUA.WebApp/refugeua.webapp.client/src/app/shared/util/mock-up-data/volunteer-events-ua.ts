import { VolunteerEvent } from "../../../core/models";
import { VolunteerEventType } from "../../../core/enums/volunteer-event-type-enum";
import { User } from "../../../core/models/user";
import { VolunteerGroup } from "../../../core/models/volunteer-group";

export const mockVolunteerEvents: VolunteerEvent[] = [
  {
    id: 201,
    title: 'Сортування гуманітарної допомоги',
    content: 'Допоможіть посортувати одяг, засоби гігієни та їжу для переселенців, зокрема для ветеранів та військових.',
    startTime: new Date('2025-06-12T10:00:00'),
    endTime: new Date('2025-06-12T15:00:00'),
    createdAt: new Date('2025-05-01'),
    addressId: 1,
    address: {
      id: 1,
      country: 'Україна',
      region: 'Київська обл.',
      district: 'Печерська громада',
      settlement: 'Київ',
      street: 'вул. Хрещатик, 22',
      postalCode: '01001'
    },
    volunteerGroupId: 1,  // Reference to VolunteerGroup
    volunteerGroup: {
      id: 1,
      title: 'Волонтерська група для ветеранів та військових',
      descriptionContent: 'Група для організації волонтерських подій для підтримки ветеранів та військових. Мета – забезпечити психологічну, фізичну та моральну підтримку тим, хто захищав країну.',
      createdAt: new Date('2025-05-10'),
      followers: [
        {
          id: 1,
          firstName: 'Олексій',
          lastName: 'Шевченко',
          email: 'oleksiy@example.com',
          phoneNumber: '+380931234567',
          dateOfBirth: new Date('1988-04-20'),
          createdAt: new Date('2023-04-01')
        }
      ],
      admins: [
        {
          id: 2,
          firstName: 'Марина',
          lastName: 'Остапенко',
          email: 'marina@example.com',
          phoneNumber: '+380501112233',
          dateOfBirth: new Date('1990-03-10'),
          createdAt: new Date('2023-01-15')
        }
      ],
      volunteerEvents: []  // Could include events related to this group
    },
    isClosed: false,
    donationLink: 'https://donate.example.com/humanitarian',
    volunteerEventType: VolunteerEventType.Participation,
    organizers: [
      {
        id: 5,
        firstName: 'Марина',
        lastName: 'Остапенко',
        email: 'marina@example.com',
        phoneNumber: '+380501112233',
        dateOfBirth: new Date('1990-03-10'),
        createdAt: new Date('2023-01-15')
      }
    ]
  },
  {
    id: 202,
    title: 'Онлайн-лекція з психологом для родин військових',
    content: 'Психологічна підтримка для родин військових. Тема: “Як підтримувати родину під час війни?”.',
    startTime: new Date('2025-06-15T19:00:00'),
    endTime: new Date('2025-06-15T20:30:00'),
    createdAt: new Date('2025-05-05'),
    volunteerGroupId: 2,  // Reference to VolunteerGroup
    volunteerGroup: {
      id: 2,
      title: 'Група допомоги для військових родин',
      descriptionContent: 'Ця група орієнтована на організацію волонтерських подій для підтримки родин військових.',
      createdAt: new Date('2025-05-15'),
      followers: [
        {
          id: 3,
          firstName: 'Тарас',
          lastName: 'Коваленко',
          email: 'taras@example.com',
          phoneNumber: '+380961234567',
          dateOfBirth: new Date('1995-10-05'),
          createdAt: new Date('2024-06-10')
        }
      ],
      admins: [
        {
          id: 4,
          firstName: 'Ірина',
          lastName: 'Марченко',
          email: 'irina@example.com',
          phoneNumber: '+380981234567',
          dateOfBirth: new Date('1987-11-20'),
          createdAt: new Date('2022-05-10')
        }
      ],
      volunteerEvents: []  // Could include events related to this group
    },
    isClosed: false,
    donationLink: '',
    volunteerEventType: VolunteerEventType.Participation,
    organizers: [
      {
        id: 6,
        firstName: 'Олександр',
        lastName: 'Кравченко',
        email: 'oleksandr@example.com',
        phoneNumber: '+380671234567',
        dateOfBirth: new Date('1985-08-22'),
        createdAt: new Date('2022-09-30')
      }
    ]
  },
  {
    id: 203,
    title: 'Волонтерська допомога родинам загиблих військових',
    content: 'Підтримка родин загиблих військових, організація допомоги та психологічної підтримки для сімей героїв.',
    startTime: new Date('2025-06-25T10:00:00'),
    endTime: new Date('2025-06-25T15:00:00'),
    createdAt: new Date('2025-06-10'),
    volunteerGroupId: 3,
    volunteerGroup: {
      id: 3,
      title: 'Група підтримки родин загиблих військових',
      descriptionContent: 'Група волонтерів, які організовують допомогу родинам загиблих військових, надають психологічну допомогу та матеріальну підтримку.',
      createdAt: new Date('2025-06-01'),
      followers: [
        {
          id: 4,
          firstName: 'Софія',
          lastName: 'Василенко',
          email: 'sofia@example.com',
          phoneNumber: '+380931234567',
          dateOfBirth: new Date('1993-07-15'),
          createdAt: new Date('2024-12-01')
        }
      ],
      admins: [
        {
          id: 5,
          firstName: 'Петро',
          lastName: 'Іванов',
          email: 'petro@example.com',
          phoneNumber: '+380681234567',
          dateOfBirth: new Date('1985-01-30'),
          createdAt: new Date('2023-03-25')
        }
      ],
      volunteerEvents: []
    },
    isClosed: false,
    donationLink: 'https://donate.example.com/fallen-soldiers',
    volunteerEventType: VolunteerEventType.Donation,
    organizers: [
      {
        id: 7,
        firstName: 'Іванна',
        lastName: 'Лисенко',
        email: 'ivanna@example.com',
        phoneNumber: '+380991234567',
        dateOfBirth: new Date('1992-12-01'),
        createdAt: new Date('2024-03-05')
      }
    ]
  },
  {
    id: 205,
    title: 'Онлайн семінар для волонтерів з психологічної підтримки',
    content: 'Семінар для волонтерів з надання психологічної підтримки людям, які пережили важкі часи. Тема: “Як правильно слухати і підтримувати в стресових ситуаціях?”.',
    startTime: new Date('2025-07-01T18:00:00'),
    endTime: new Date('2025-07-01T19:30:00'),
    createdAt: new Date('2025-06-20'),
    volunteerGroupId: 5,
    volunteerGroup: {
      id: 5,
      title: 'Психологічна підтримка для волонтерів',
      descriptionContent: 'Група волонтерів, що надають психологічну підтримку постраждалим, а також навчання для інших волонтерів, як правильно надавати допомогу.',
      createdAt: new Date('2025-06-15'),
      followers: [
        {
          id: 8,
          firstName: 'Ірина',
          lastName: 'Данилова',
          email: 'irina.danilova@example.com',
          phoneNumber: '+380674563210',
          dateOfBirth: new Date('1990-04-05'),
          createdAt: new Date('2024-12-15')
        }
      ],
      admins: [
        {
          id: 9,
          firstName: 'Ганна',
          lastName: 'Коваль',
          email: 'anna.koval@example.com',
          phoneNumber: '+380992345678',
          dateOfBirth: new Date('1986-11-12'),
          createdAt: new Date('2023-11-01')
        }
      ],
      volunteerEvents: []
    },
    isClosed: false,
    donationLink: '',
    volunteerEventType: VolunteerEventType.Participation,
    organizers: [
      {
        id: 10,
        firstName: 'Олександра',
        lastName: 'Семенова',
        email: 'olga.semenova@example.com',
        phoneNumber: '+380991234567',
        dateOfBirth: new Date('1992-06-22'),
        createdAt: new Date('2024-07-25')
      }
    ]
  },
  {
    id: 206,
    title: 'Волонтерська допомога для дітей з обмеженими можливостями',
    content: 'Організація заходу для дітей з обмеженими можливостями, допомога в адаптації до нових умов, психологічна та фізична підтримка.',
    startTime: new Date('2025-07-05T09:00:00'),
    endTime: new Date('2025-07-05T13:00:00'),
    createdAt: new Date('2025-06-22'),
    volunteerGroupId: 6,
    volunteerGroup: {
      id: 6,
      title: 'Група допомоги дітям з обмеженими можливостями',
      descriptionContent: 'Група волонтерів, які надають допомогу дітям з особливими потребами, проводять активності для їхнього розвитку та адаптації.',
      createdAt: new Date('2025-06-18'),
      followers: [
        {
          id: 11,
          firstName: 'Марія',
          lastName: 'Шевченко',
          email: 'maria.shevchenko@example.com',
          phoneNumber: '+380998765432',
          dateOfBirth: new Date('1991-09-13'),
          createdAt: new Date('2024-08-20')
        }
      ],
      admins: [
        {
          id: 12,
          firstName: 'Вікторія',
          lastName: 'Мельник',
          email: 'viktoriya.melnik@example.com',
          phoneNumber: '+380991234567',
          dateOfBirth: new Date('1989-07-11'),
          createdAt: new Date('2023-06-15')
        }
      ],
      volunteerEvents: []
    },
    isClosed: false,
    donationLink: 'https://donate.example.com/children-support',
    volunteerEventType: VolunteerEventType.Participation,
    organizers: [
      {
        id: 13,
        firstName: 'Сергій',
        lastName: 'Петренко',
        email: 'serhiy.petrenko@example.com',
        phoneNumber: '+380991234567',
        dateOfBirth: new Date('1990-03-15'),
        createdAt: new Date('2023-05-25')
      }
    ]
  }
];
