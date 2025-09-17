import { PsychologistInformation, User } from "../../../core/models";


export const mockPsychologists: PsychologistInformation[] = [
  {
    id: 1,
    title: 'Психологічна підтримка ветеранів',
    description: `Пропоную індивідуальні сесії для ветеранів бойових дій, що допомагають справлятися з наслідками ПТСР. 
Сеанси проходять у спокійному та підтримуючому середовищі з урахуванням досвіду клієнта та його потреб. 
Спеціалізуюсь на роботі з тривожністю, відчуттям провини та емоційним вигоранням.`,
    createdAt: new Date('2025-05-01'),
    author: { id: 2, firstName: 'Марина', lastName: 'Остапенко' } as User,
    contactId: 1,
    contact: {
      id: 1,
      phoneNumber: '+380931234567',
      email: 'veteran.psych@example.com',
      telegram: '@psy_veteran',
      facebook: 'https://facebook.com/psy.veteran'
    }
  },
  {
    id: 2,
    title: 'Онлайн-консультації для ВПО',
    description: `Психологічна допомога для внутрішньо переміщених осіб. Працюємо з почуттям втрати, стресом через переїзд і зміни. 
Консультації проходять онлайн — зручний формат для тих, хто ще шукає стабільності або житло. Можливі короткострокові та довгострокові програми.`,
    createdAt: new Date('2025-04-27'),
    author: { id: 3, firstName: 'Іван', lastName: 'Петренко' } as User,
    contactId: 2,
    contact: {
      id: 2,
      phoneNumber: '+380507654321',
      email: 'vpo.support@example.com',
      telegram: '@psy_vpo',
      viber: '+380507654321'
    }
  },
  {
    id: 3,
    title: 'Дитячий психолог у Києві',
    description: `Психологічна допомога дітям, які пережили травматичні події: війна, евакуація, втрата близьких. 
Спільно з батьками розробляємо план підтримки. Сеанси проводяться в дружній атмосфері з ігровими методами терапії.`,
    createdAt: new Date('2025-05-05'),
    author: { id: 4, firstName: 'Олена', lastName: 'Гончар' } as User,
    contactId: 3,
    contact: {
      id: 3,
      phoneNumber: '+380931112233',
      telegram: '@kids_help',
      facebook: 'https://facebook.com/psy.kids'
    }
  },
  {
    id: 4,
    title: 'Групова терапія для жінок',
    description: `Запрошую жінок на групові зустрічі для відновлення емоційної стабільності після стресових подій. 
У фокусі – підтримка одна одної, самоприйняття, робота з внутрішніми бар’єрами. Групи до 10 учасниць.`,
    createdAt: new Date('2025-04-15'),
    author: { id: 5, firstName: 'Ірина', lastName: 'Коваленко' } as User,
    contactId: 4,
    contact: {
      id: 4,
      phoneNumber: '+380509998877',
      email: 'women.group@example.com'
    }
  },
  {
    id: 5,
    title: 'Підтримка для підлітків',
    description: `Працюю з підлітками, що зіткнулися з тривогою, ізоляцією або проблемами в навчанні. 
Спрямовую до конструктивних форм комунікації, самовираження та розвитку особистісних ресурсів.`,
    createdAt: new Date('2025-03-30'),
    author: { id: 6, firstName: 'Антон', lastName: 'Литвин' } as User,
    contactId: 5,
    contact: {
      id: 5,
      phoneNumber: '+380937777777',
      telegram: '@psy_teen'
    }
  },
  {
    id: 6,
    title: 'Терапія втрати',
    description: `Надаю допомогу людям, які втратили близьких під час війни. 
Робота з горем, прийняттям та адаптацією до нового життя. Без осуду, у безпечному середовищі.`,
    createdAt: new Date('2025-05-07'),
    author: { id: 7, firstName: 'Наталя', lastName: 'Мельник' } as User,
    contactId: 6,
    contact: {
      id: 6,
      phoneNumber: '+380937111222',
      facebook: 'https://facebook.com/therapyloss'
    }
  },
  {
    id: 7,
    title: 'Кризове консультування',
    description: `Допомагаю впоратися з емоційними кризами, панічними атаками, станами відчаю. 
Швидкий контакт та підтримка в перші години після складної події.`,
    createdAt: new Date('2025-05-08'),
    author: { id: 8, firstName: 'Тарас', lastName: 'Романюк' } as User,
    contactId: 7,
    contact: {
      id: 7,
      phoneNumber: '+380935551122',
      telegram: '@crisis_psy'
    }
  },
  {
    id: 8,
    title: 'Медитації та тілесні практики',
    description: `Працюю на перетині психології та тілесних практик. Медитації, дихальні вправи, елементи йоги. 
Індивідуальний підхід до відновлення внутрішнього балансу.`,
    createdAt: new Date('2025-05-09'),
    author: { id: 9, firstName: 'Леся', lastName: 'Кривенко' } as User,
    contactId: 8,
    contact: {
      id: 8,
      phoneNumber: '+380508888999',
      viber: '+380508888999'
    }
  },
  {
    id: 9,
    title: 'Психоедукація для батьків',
    description: `Навчальні мінікурси для батьків дітей, які пережили війну або евакуацію. 
Як підтримати дитину, зберігаючи власну стабільність та ресурсність.`,
    createdAt: new Date('2025-05-10'),
    author: { id: 10, firstName: 'Юлія', lastName: 'Демченко' } as User,
    contactId: 9,
    contact: {
      id: 9,
      phoneNumber: '+380936661122',
      email: 'parent.edu@example.com'
    }
  },
  {
    id: 10,
    title: 'Психотерапія для подружніх пар',
    description: `Пропоную підтримку для пар, які переживають труднощі у зв’язку, відстанню чи адаптацією після повернення з фронту. 
Працюю над побудовою довіри, відкритої комунікації та вирішенням конфліктів.`,
    createdAt: new Date('2025-05-11'),
    author: { id: 11, firstName: 'Микола', lastName: 'Сидорчук' } as User,
    contactId: 10,
    contact: {
      id: 10,
      phoneNumber: '+380931000200',
      telegram: '@couple.therapy'
    }
  }
];
