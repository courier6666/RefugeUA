import { TimeSpan } from "./time-span"

export const getLabelForTimeSpanDays = (ts: TimeSpan): string => {
    const x = ts.days % 10;
    switch(true)
    {
        case (x == 0):
            return 'днів';
            break;
        case (x == 1):
            return 'день';
        case (x < 5):
            return 'дні';
        case (x < 9):
            return 'днів';
    }

    return 'днів';
}