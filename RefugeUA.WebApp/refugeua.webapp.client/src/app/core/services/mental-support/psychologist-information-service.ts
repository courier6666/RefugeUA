import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, catchError, map } from "rxjs";
import { PsychologistInformation } from "../../models";
import { CreatePsychologistInfoCommand } from "../../api-models/create-psychologist-info-command";
import { PagingInfo } from "../../../shared/util/paging-model";
import { Params } from "@angular/router";
import { catchErrorForFormMethod } from "../../../shared/util/form-error-capture-method";

@Injectable({
    providedIn: 'root'
})
export class PsychologistInformationService {

    constructor(private http: HttpClient) {

    }

    getPsychologistInformationById(id: number): Observable<PsychologistInformation> {
        return this.http.get<PsychologistInformation>(`api/mental-support/psychologist-informations/${id}`);
    }

    createPsychologistInformation(command: CreatePsychologistInfoCommand): Observable<any> {
        return this.http.post(`api/mental-support/psychologist-informations`, command).pipe(map(res => {
            return res;
        }),
            catchError(catchErrorForFormMethod));
    }

    editPsychologistInformation(id: number, command: CreatePsychologistInfoCommand): Observable<any> {
        return this.http.put(`api/mental-support/psychologist-informations/${id}`, command).pipe(map(res => {
            return res;
        }),
            catchError(catchErrorForFormMethod));
    }

    deletePsychologistInformation(id: number): Observable<any> {
        return this.http.delete(`api/mental-support/psychologist-informations/${id}`);
    }

    getPsychologistInformations(page: number, pageLength: number, prompt?: string | null): Observable<PagingInfo<PsychologistInformation>> {
        let params: Params = {
            page: page,
            pageLength: pageLength
        };

        if (prompt != null && prompt != undefined && prompt.length > 0)
            params['prompt'] = prompt ? prompt : null;

        return this.http.get<PagingInfo<PsychologistInformation>>(`api/mental-support/psychologist-informations/paged`, { params: params });
    }
}