

 

export interface AccessToken { 
    AccessTokenId: number;
    Token: string;
    UserName: string;
    EffectiveTime: Date;
    ExpiresIn: number;
    UserAgent: string;
    IP: string;
}