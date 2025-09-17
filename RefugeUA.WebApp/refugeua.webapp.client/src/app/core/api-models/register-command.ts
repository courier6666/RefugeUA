export interface RegisterCommand {
    firstName: string,
    lastName: string,
    role: string,
    dateOfBirth: Date,
    email: string,
    phoneNumber: string,
    password: string,
    confirmPassword: string,
    district?: string
}