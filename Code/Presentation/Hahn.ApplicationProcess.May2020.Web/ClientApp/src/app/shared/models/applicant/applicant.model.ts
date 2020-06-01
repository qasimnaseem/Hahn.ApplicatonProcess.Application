import { AuditableEntity } from '../common/auditable-entity.model';

export class Applicant extends AuditableEntity {

    applicantId: number;
    name: string;
    familyName: string;
    countryOfOrigin: string;
    address: string;
    emailAddress: string;
    hired: boolean;
    age: number;

    public constructor(init?: Partial<Applicant>) {
        super(init);
        Object.assign(this, init);
    }
}
