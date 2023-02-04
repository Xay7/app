export class JwtToken {
  public payload:
    | {
        sub: string;
        client_id?: string;
        exp: number;
        iat: number;
        iss: string;
        name: string;
        role: string | string[];
        scope: string;
        username: string;
      }
    | undefined;

  constructor(public value: string) {
    if (value && value !== '') {
      const base64Url = value.split('.')[1];
      const base64 = base64Url.replace('-', '+').replace('_', '/');
      this.payload = JSON.parse(window.atob(base64));
    } else {
      this.value = '';
    }
  }

  public static TryCreate(value: string): JwtToken | undefined {
    if (!value || value === '') return undefined;
    try {
      return new JwtToken(value);
    } catch (ex) {
      return undefined;
    }
  }

  get expirationDate(): Date {
    if (!this.payload) {
      throw 'Token has invalid payload';
    }
    return new Date(this.payload.exp * 1000);
  }

  get isValid(): boolean {
    if (this.payload !== null) {
      const now = new Date();
      return now < this.expirationDate;
    }
    return false;
  }

  get isExpired(): boolean {
    if (!this.payload) {
      throw 'Token has invalid payload';
    }
    const expirationDate = new Date(this.payload.exp * 1000);
    const now = new Date();
    return now > expirationDate;
  }
}
