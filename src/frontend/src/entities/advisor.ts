export enum HeathStatusEnum
{
    Green,
    Yellow,
    Red
}

export class Advisor {
    constructor(public sin: string, public name: string, public phone: string, public address: string, public heathStatus:HeathStatusEnum = HeathStatusEnum.Green){
        this.phone = phone?.replace(/\D+/g, '');
        this.sin = sin?.replace(/\D+/g, '');
    }
    
    public getMaskedPhone():string{
        return this.phone !== "" && this.phone !== undefined ? this.format(this.phone, "####-####") : '';
    }

    public getMaskedSin():string{
        return this.sin !== "" && this.sin !== undefined ? this.format(this.sin, "###-###-###") : '';
    }

    private format(value:string, pattern:string){
        let i = 0;
        const v = value.toString();
        return pattern.replace(/#/g, _ => v[i++]);
    }

}