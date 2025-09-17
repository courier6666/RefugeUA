export interface EditProfileCommand {
    firstName: string,
    lastName: string,
    dateOfBirth: Date,
    phoneNumber: string,
    district?: string,
    profilePicture: File | null
}