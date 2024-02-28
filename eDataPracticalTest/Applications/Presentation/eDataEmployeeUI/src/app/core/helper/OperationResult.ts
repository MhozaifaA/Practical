import { HttpAppService } from "../services/http-app.service";


export class OperationResultBase {

  protected Instance: HttpAppService | null = null;

  constructor(instance: HttpAppService) {
    this.Instance = instance;
  }
}

export class OperationResult<T> extends OperationResultBase {
  public result?: T | null;

  constructor(instance: HttpAppService) {
    super(instance)
  }

  
  public handleResult: ((data?: T) => void) = () => { };

  public Result(result: ((data?: T) => void)) {
    this.handleResult = result;
    return this;
  }

  public handleError: ((error?: any) => void) = () => { };

  public Error(result: ((error?: any) => void)) {
    this.handleError = result;
    return this;
  }

}
