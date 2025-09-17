import { Observable, throwError } from "rxjs"

export const catchErrorForFormMethod = (err: any, caught: Observable<Object>): Observable<never> => {
    let errorMessage = "";

    let errors: string[] = [];

    if (err.status == 400 && err.error.errors && Object.keys(err.error.errors).length > 0) {
        for (let field in err.error.errors) {
            errors.push(...err.error.errors[field]);
        }
        errorMessage = errors.join('\n');
    }

    return throwError(() => new Error(errorMessage));
}