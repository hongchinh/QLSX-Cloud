using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSX.Shared.Constants
{
    public static class ApiResponseMessages
    {
        public const string Invalid = "無効な情報";
        public const string DuplicatedTenantcode = "この企業コードは既に存在しています。";
        public const string DuplicatedTenantMail = "入力したメールアドレスがすでに利用されています。";
        public const string DuplicatedContractTypeName = "入力した名前は既に存在します。";
        public const string NotFoundContractTypeBasic = "契約種別を選択してください Basic。";
        public const string Success = "成功しました。";
        public const string UserNotFound = "このユーザーは既に存在していません。";
        public const string TenantNotFound = "入力された企業は存在しません。";
        public const string ContractTypetNotFound = "契約種別を選択してください。";
        public const string BadRequest = "不正な要求です。";
        public const string Unauthorized = "許可されていない";
        public const string MissingTenant = "企業の情報がありません。 管理者に連絡してください。";
        public const string RootEmailExist = "不正な要求です。";
        public const string InCorrectEmailOrPassword = "ログインIDまたはパスワードが間違っています。もう一度お試しください。";
        public const string UserNameExisted = "このユーザーは既に存在しています。";
        public const string RequireLoginAgain = "ログアウトして再度ログインしてください。";
        public const string DuplicatedUserName = "このユーザーは既に存在しています。";
        public const string OldPasswordNotCorrect = "旧パスワードは正しくありません。";
        public const string ConfPasswordNotCorrect = "パスワードとパスワードの再入力が一致しません。再入力してください";
        public const string UserLimitTenant = "ユーザー数が制限を超えています。";
        public const string ValidateDateTenant = "契約終了日は利用開始日より大きくする必要があります。";
        public const string LoginFalse = "または会社の契約が切れています";
        public const string AlreadySendRequest = "ホワイトリスト追加申請を送信しました。管理者の承認をお待ちください。";
        public const string TenantDisable = "本企業が無効化しております。管理者に連絡してください。";
        public const string EmailNotFound = "入力したメールアドレスが有効なメールアドレスではありません。";
        public const string TenantNameExist = "入力した企業名がすでに存在しています。";
        public const string DownloadFalse = "エラーが発生しました。しばらくしてからもう一度お試しください。";
        public const string ConnectionEmailFail = "エラーが発生しました。しばらくしてからもう一度お試しください。";
        public const string DeleteContractTypeError = " を適用している企業があり、削除できません。";
        public const string ErrorDeleteTenant = "本企業はクレジットカード自動課金を利用しています。自動課金を停止する前に企業を削除できません。";
        public const string DeleteContractTypeErrorActiveBasic = "有効な契約種別は削除できません";
        public const string NoteTextUpdateLicense = "基本ライセンス数差あり";
        public const string DataNotFound = "データが見つかりません。";
        public const string TenantNotHaveContractBasic = "基本サービスが登録されていません。";
        public const string APIFsecureFaild = "Endpoint APIでエラーが発生しました。しばらくしてからもう一度お試しください。";
        public const string RobotPaymentStopError = "利用停止が完了しましたが、自動課金停止にエラーが発生しました。Robot Paymentサイトで自動課金停止してください。";
        public const string RobotPaymentUpdatePriceError = "サービスの変更は正常に保存されました。ただし、金額を変更のAPIにエラーが発生しました。Robot Paymentサイトで金額を変更してください。";
        public const string TenantSettingSaveSuccess = "保存が成功しました。";
        public const string TenantSettingSaveFailed = "エラーが発生しました。";
    }
}
