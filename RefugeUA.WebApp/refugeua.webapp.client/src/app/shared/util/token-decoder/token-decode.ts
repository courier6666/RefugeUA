import { ɵɵngDeclareNgModule } from "@angular/core";
import { UserClaims } from "../../../core/api-models/user-claims";
import { jwtDecode } from "jwt-decode";

const userClaimTypes = {
    identifier: 'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier',
    email: 'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress',
    name: 'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name',
    role: 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role',
    district: 'district',
    phoneNumber: 'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/mobilephone'
}

export const decodeToken = (token: string): UserClaims => {
    try {
        let claims: UserClaims = {
            id: '',
            email: '',
            name: '',
            role: '',
            district: '',
            phoneNumber: '',
            exp: 0,
        }

        let decodedData = jwtDecode(token);
        let decoded = decodedData as { [key: string]: any };
        console.log(decoded);


        if (!decoded[userClaimTypes.identifier]) {
            throw new Error("Invalid token provided!");
        }

        claims.id = decoded[userClaimTypes.identifier];

        if (!decoded[userClaimTypes.email]) {
            throw new Error("Invalid token provided!");
        }

        claims.email = decoded[userClaimTypes.email];

        if (!decoded[userClaimTypes.name]) {
            throw new Error("Invalid token provided!");
        }

        claims.name = decoded[userClaimTypes.name];

        if (!decoded[userClaimTypes.role]) {
            throw new Error("Invalid token provided!");
        }

        claims.role = decoded[userClaimTypes.role];

        if (!decoded[userClaimTypes.district] && decoded[userClaimTypes.district] != '') {
            throw new Error("Invalid token provided!");
        }

        console.log(claims);

        claims.district = decoded[userClaimTypes.district];

        if (!decoded[userClaimTypes.phoneNumber]) {
            throw new Error("Invalid token provided!");
        }

        claims.phoneNumber = decoded[userClaimTypes.phoneNumber];

        if (!decoded['exp']) {
            throw new Error("Invalid token provided!");
        }

        claims.exp = decoded['exp'];

        return claims
    } catch (error) {
        throw error;
    }

}