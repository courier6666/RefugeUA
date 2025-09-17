import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, map } from "rxjs";

@Injectable({
    providedIn: 'root',
})
export class FileService {

    constructor(private http: HttpClient) {

    }

    public getImage(filename: string): Observable<File> {
        return this.http.get(`api/images/${filename}`, { responseType: 'blob' }).pipe(
            map(blob => {
                const file = new File([blob], filename, { type: blob.type });
                return file;
            })
        );
    }

    public getApiPath(filename: string): string {
        return `api/images/${filename}`;
    }
}