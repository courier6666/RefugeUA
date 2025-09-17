import { VolunteerEvent } from "../../../core/models"
import { VolunteerEventType } from "../../../core/enums/volunteer-event-type-enum"

export const mockVolunteerEvent: VolunteerEvent = {
  id: 201,
  title: 'Сортування гуманітарної допомоги',
  content: `Запрошуємо всіх охочих долучитися до важливої волонтерської ініціативи з сортування гуманітарної допомоги для внутрішньо переміщених осіб, ветеранів та військовослужбовців. Основне завдання — посортувати речі, які надходять від благодійників з різних куточків України та Європи.

У рамках заходу ми працюватимемо з:
- Одягом: чоловічим, жіночим, дитячим (враховуючи сезонність та розміри)
- Засобами гігієни: миючі засоби, серветки, підгузки, зубні щітки та пасти тощо
- Продуктами харчування тривалого зберігання: крупи, консерви, вода, сухі сніданки

Ми надаємо:
- Засоби індивідуального захисту (рукавички, маски, антисептики)
- Харчування для волонтерів (перекус, чай, кава)
- Сертифікат участі для студентів або охочих додати досвід до волонтерського резюме

Приймати участь можуть особи віком від 16 років. Бажано одягнутись у зручний одяг і взуття, що не шкода забруднити. У приміщенні працює кондиціонер, доступні туалети та зона відпочинку.

Усі зібрані речі після сортування будуть передані в логістичні пункти громад, з якими ми співпрацюємо, та розподілені відповідно до потреб реєстрованих отримувачів.

Ваш вклад — неоціненний. Навіть кілька годин можуть змінити життя тих, хто постраждав через війну. Разом — сильніші!`,
  startTime: new Date('2025-06-12T10:00:00'),
  endTime: new Date('2025-06-12T15:00:00'),
  createdAt: new Date('2025-05-01T08:00:00'),
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
  scheduleItems: [
    {
      id: 1,
      startTime: new Date('2025-06-12T10:00:00'),
      description: 'Реєстрація учасників, видача бейджів і вступне слово організаторів',
      volunteerEventId: 201
    },
    {
      id: 2,
      startTime: new Date('2025-06-12T10:30:00'),
      description: 'Розподіл по зонах сортування та інструктаж з техніки безпеки',
      volunteerEventId: 201
    },
    {
      id: 3,
      startTime: new Date('2025-06-12T11:00:00'),
      description: 'Початок роботи: сортування одягу, гігієнічних засобів, продуктів',
      volunteerEventId: 201
    },
    {
      id: 4,
      startTime: new Date('2025-06-12T13:00:00'),
      description: 'Перерва на обід (чай, кава, перекус)',
      volunteerEventId: 201
    },
    {
      id: 5,
      startTime: new Date('2025-06-12T13:30:00'),
      description: 'Продовження роботи в зонах сортування',
      volunteerEventId: 201
    },
    {
      id: 6,
      startTime: new Date('2025-06-12T15:00:00'),
      description: 'Підбиття підсумків, вручення сертифікатів, групове фото',
      volunteerEventId: 201
    }
  ],
  volunteerGroupId: 1,
  volunteerGroup: {
    id: 1,
    title: 'Волонтерська група для ветеранів та військових',
    descriptionContent: 'Група для організації волонтерських подій для підтримки ветеранів та військових. Мета – забезпечити психологічну, фізичну та моральну підтримку тим, хто захищав країну.',
    createdAt: new Date('2025-05-10T10:00:00'),
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
    volunteerEvents: []
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
};
