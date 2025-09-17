import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from '../../models';
import { Observable } from 'rxjs';
import { Params } from '@angular/router';
import { PagingInfo } from '../../../shared/util/paging-model';
import { EditProfileCommand } from '../../api-models/edit-profile-command';

@Injectable({
    providedIn: 'root'
})
export class UserService {
    constructor(private http: HttpClient) {

    }

    getAllUsersAdmin(page: number, pageLength: number, prompt?: string | null): Observable<PagingInfo<User>> {
        let params: Params = {
            page: page,
            pageLength: pageLength
        };

        if (prompt != null && prompt != undefined && prompt.length > 0)
            params['prompt'] = prompt ? prompt : null;

        return this.http.get<PagingInfo<User>>('api/admin/users/paged', { params: params });
    }

    acceptUser(userId: number): Observable<any> {
        return this.http.patch(`api/auth/users/${userId}/accept`, {});
    }

    forbidUser(userId: number): Observable<any> {
        return this.http.patch(`api/auth/users/${userId}/forbid`, {});
    }

    deleteUser(userId: number): Observable<any> {
        return this.http.delete(`api/admin/users/${userId}`);
    }

    confirmEmailUser(userId: number): Observable<any> {
        return this.http.patch(`api/auth/users/${userId}/confirm-email-for-user`, {});
    }

    getMyProfile(): Observable<User> {
        return this.http.get<User>(`api/profile/mine`);
    }

    getUserProfile(id: number): Observable<User> {
        return this.http.get<User>(`api/users/${id}/profile`)
    }

    editMyProfile(editProfileCommand: EditProfileCommand): Observable<any> {
        let formData: FormData = new FormData();

        formData.append('firstName', editProfileCommand.firstName);
        formData.append('lastName', editProfileCommand.lastName);
        formData.append('dateOfBirth', editProfileCommand.dateOfBirth.toString());
        formData.append('phoneNumber', editProfileCommand.phoneNumber);

        if (editProfileCommand.district) {
            formData.append('district', editProfileCommand.district);
        }

        if (editProfileCommand.profilePicture) {
            formData.append('profilePicture', editProfileCommand.profilePicture, editProfileCommand.profilePicture.name);
        }


        return this.http.put(`api/profile`, formData);
    }

    editUserProfile(id: number, editProfileCommand: EditProfileCommand): Observable<any> {
        let formData: FormData = new FormData();

        formData.append('firstName', editProfileCommand.firstName);
        formData.append('lastName', editProfileCommand.lastName);
        formData.append('dateOfBirth', editProfileCommand.dateOfBirth.toString());
        formData.append('phoneNumber', editProfileCommand.phoneNumber);

        if (editProfileCommand.district) {
            formData.append('district', editProfileCommand.district);
        }

        if (editProfileCommand.profilePicture) {
            formData.append('profilePicture', editProfileCommand.profilePicture, editProfileCommand.profilePicture.name);
        }

        return this.http.put(`api/admin/users/${id}`, formData);
    }
}