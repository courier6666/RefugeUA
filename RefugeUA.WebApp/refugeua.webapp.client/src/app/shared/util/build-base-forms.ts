import { FormGroup, FormBuilder, Validators, FormControl } from "@angular/forms";


export const baseAnnouncementForm = (): FormGroup => {
    let fb = new FormBuilder();

    return fb.group({
        title: ['', [Validators.required, Validators.maxLength(100)]],
        content: ['', [Validators.required, Validators.maxLength(4096)]],
        contactInformation: fb.group({
            phoneNumber: ['', [Validators.required, Validators.pattern(/^\+?[1-9]\d{10,14}$/)]],
            email: ['', [Validators.email, Validators.maxLength(128)]],
            telegram: ['', Validators.maxLength(256)],
            viber: ['', Validators.maxLength(256)],
            facebook: ['', Validators.maxLength(256)]
        }),
        address: fb.group({
            country: ['', [Validators.required, Validators.maxLength(100)]],
            region: ['', [Validators.required, Validators.maxLength(100)]],
            district: ['', [Validators.required, Validators.maxLength(100)]],
            settlement: ['', [Validators.required, Validators.maxLength(100)]],
            street: ['', [Validators.required, Validators.maxLength(100)]],
            postalCode: ['', [Validators.required, Validators.pattern(/^\d{5}$/)]]
        })
    });
}

export const baseAddressFormNotRequired = (): FormGroup => {
    let fb = new FormBuilder();

    return fb.group({
        country: ['', [Validators.required, Validators.maxLength(100)]],
        region: ['', [Validators.required, Validators.maxLength(100)]],
        district: ['', [Validators.required, Validators.maxLength(100)]],
        settlement: ['', [Validators.required, Validators.maxLength(100)]],
        street: ['', [Validators.required, Validators.maxLength(100)]],
        postalCode: ['', Validators.pattern(/^\d{5}$/)]
    });
}

export const baseAddressFormRequired = (): FormGroup => {
    let fb = new FormBuilder();

    return fb.group({
        country: ['', [Validators.required, Validators.maxLength(100)]],
        region: ['', [Validators.required, Validators.maxLength(100)]],
        district: ['', [Validators.required, Validators.maxLength(100)]],
        settlement: ['', [Validators.required, Validators.maxLength(100)]],
        street: ['', [Validators.required, Validators.maxLength(100)]],
        postalCode: ['', [Validators.required, Validators.pattern(/^\d{5}$/)]]
    });
}

export const baseContactInfoFormRequired = (): FormGroup => {
    let fb = new FormBuilder();

    return fb.group({
        phoneNumber: new FormControl<string | null>(null, Validators.required),
        email: new FormControl<string | null>(null, [Validators.email, Validators.maxLength(128)]),
        telegram: new FormControl<string | null>(null, Validators.maxLength(256)),
        viber: new FormControl<string | null>(null, Validators.maxLength(256)),
        facebook: new FormControl<string | null> (null, Validators.maxLength(256))
    });
}