export class AuditableEntity {

    createdOn : Date;
    modifiedOn: Date;

    public constructor(init?: Partial<AuditableEntity>) {
        Object.assign(this, init);
    }
}
