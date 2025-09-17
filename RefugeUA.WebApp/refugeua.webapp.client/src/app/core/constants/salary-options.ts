export interface SalaryOption {
    value: number;
    label: string;
    count?: number;
  }
  

export const salaryOptionsLower: SalaryOption[] = [
    { value: 10000, label: '10 000 грн' },
    { value: 15000, label: '15 000 грн' },
    { value: 20000, label: '20 000 грн' },
    { value: 30000, label: '30 000 грн' },
    { value: 40000, label: '40 000 грн' },
    { value: 50000, label: '50 000 грн' },
    { value: 100000, label: '100 000 грн і більше' }
];

export const salaryOptionsUpper: SalaryOption[] = [
    { value: 10000, label: '10 000 грн' },
    { value: 15000, label: '15 000 грн' },
    { value: 20000, label: '20 000 грн' },
    { value: 30000, label: '30 000 грн' },
    { value: 40000, label: '40 000 грн' },
    { value: 50000, label: '50 000 грн' },
    { value: 100000, label: '100 000 грн' }
];