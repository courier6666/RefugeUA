export interface AccomodationPrice {
    value: number,
    label: string
}

export const accomodationPriceOptionsLower: AccomodationPrice[] = [
    { value: 2000, label: '2 000 грн' },
    { value: 4000, label: '4 000 грн' },
    { value: 6000, label: '6 000 грн' },
    { value: 8000, label: '8 000 грн' },
    { value: 10000, label: '10 000 грн' },
    { value: 15000, label: '15 000 грн' },
    { value: 20000, label: '20 000 грн і більше' }
];

export const accomodationPriceOptionsUpper: AccomodationPrice[] = [
    { value: 2000, label: '2 000 грн' },
    { value: 4000, label: '4 000 грн' },
    { value: 6000, label: '6 000 грн' },
    { value: 8000, label: '8 000 грн' },
    { value: 10000, label: '10 000 грн' },
    { value: 15000, label: '15 000 грн' },
    { value: 20000, label: 'до 20 000 грн' }
];
