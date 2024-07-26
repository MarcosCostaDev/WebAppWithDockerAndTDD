export class Advisor {

    private _sin: string;
    private _name: string;
    private _phone: string;
    private _address: string;

    constructor(sin: string, name: string, phone: string, address: string){
        this._sin = sin;
        this._address = address;
        this._name = name;
        this._phone = phone;
    }


    public get sin(): string {
        return this._sin;
    }

    public set sin(value: string) {
        this._sin = value;
    }

    public get name(): string {
        return this._name;
    }
    public set name(value: string) {
        this._name = value;
    }
    public get phone(): string {
        return this._phone;
    }
    public set phone(value: string) {
        this._phone = value;
    }
    public get address(): string {
        return this._address;
    }
    public set address(value: string) {
        this._address = value;
    }


    public getMaskedPhone():string{
        return this.format(this.phone, "####-####");
    }

    public getMaskedSin():string{
        return this.format(this.sin, "###-###-###");
    }

    private format(value:string, pattern:string){
        let i = 0;
        const v = value.toString();
        return pattern.replace(/#/g, _ => v[i++]);
    }

}