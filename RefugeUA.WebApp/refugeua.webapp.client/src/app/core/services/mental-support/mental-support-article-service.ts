import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, catchError, map, pipe } from "rxjs";
import { MentalSupportArticle } from "../../models";
import { CreateMentalSupportArticleCommand } from "../../api-models/create-mental-support-article-command";
import { PagingInfo } from "../../../shared/util/paging-model";
import { Params } from "@angular/router";
import { catchErrorForFormMethod } from "../../../shared/util/form-error-capture-method";

@Injectable({
    providedIn: 'root'
})
export class MentalSupportArticleService {
    constructor(private http: HttpClient) {

    }

    getArticleById(id: number): Observable<MentalSupportArticle> {
        return this.http.get<MentalSupportArticle>(`api/mental-support/articles/${id}`);
    }

    deleteArticle(id: number): Observable<any> {
        return this.http.delete(`api/mental-support/articles/${id}`);
    }

    createArticle(command: CreateMentalSupportArticleCommand): Observable<any> {
        return this.http.post(`api/mental-support/articles`, command).pipe(map(res => {
            return res;
        }),
            catchError(catchErrorForFormMethod));
    }

    editArticle(id: number, command: CreateMentalSupportArticleCommand): Observable<any> {
        return this.http.put(`api/mental-support/articles/${id}`, command).pipe(map(res => {
            return res;
        }),
            catchError(catchErrorForFormMethod));
    }

    getArticles(page: number, pageLength: number, prompt?: string | null): Observable<PagingInfo<MentalSupportArticle>> {
        let params: Params = {
            page: page,
            pageLength: pageLength
        };

        if (prompt != null && prompt != undefined && prompt.length > 0)
            params['prompt'] = prompt ? prompt : null;

        return this.http.get<PagingInfo<MentalSupportArticle>>(`api/mental-support/articles/paged`, { params: params });
    }
}