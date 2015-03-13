using System;
using Foundation;
using UIKit;

namespace AgileBits
{
    // @interface OnePasswordExtension : NSObject
    [BaseType (typeof(NSObject))]
    interface OnePasswordExtension
    {
        // +(OnePasswordExtension *)sharedExtension;
        [Static]
        [Export ("sharedExtension")]
        OnePasswordExtension SharedExtension { get; }

        // -(BOOL)isAppExtensionAvailable;
        [Export ("isAppExtensionAvailable")]
        bool IsAppExtensionAvailable { get; }

        // -(void)findLoginForURLString:(NSString *)URLString forViewController:(UIViewController *)viewController sender:(id)sender completion:(void (^)(NSDictionary *, NSError *))completion;
        [Export ("findLoginForURLString:forViewController:sender:completion:")]
        void FindLoginForURLString (string URLString, UIViewController viewController, NSObject sender, Action<NSDictionary, NSError> completion);

        // -(void)storeLoginForURLString:(NSString *)URLString loginDetails:(NSDictionary *)loginDetailsDict passwordGenerationOptions:(NSDictionary *)passwordGenerationOptions forViewController:(UIViewController *)viewController sender:(id)sender completion:(void (^)(NSDictionary *, NSError *))completion;
        [Export ("storeLoginForURLString:loginDetails:passwordGenerationOptions:forViewController:sender:completion:")]
        void StoreLoginForURLString (string URLString, NSDictionary loginDetailsDict, NSDictionary passwordGenerationOptions, UIViewController viewController, NSObject sender, Action<NSDictionary, NSError> completion);

        // -(void)changePasswordForLoginForURLString:(NSString *)URLString loginDetails:(NSDictionary *)loginDetailsDict passwordGenerationOptions:(NSDictionary *)passwordGenerationOptions forViewController:(UIViewController *)viewController sender:(id)sender completion:(void (^)(NSDictionary *, NSError *))completion;
        [Export ("changePasswordForLoginForURLString:loginDetails:passwordGenerationOptions:forViewController:sender:completion:")]
        void ChangePasswordForLoginForURLString (string URLString, NSDictionary loginDetailsDict, NSDictionary passwordGenerationOptions, UIViewController viewController, NSObject sender, Action<NSDictionary, NSError> completion);

        // -(void)fillLoginIntoWebView:(id)webView forViewController:(UIViewController *)viewController sender:(id)sender completion:(void (^)(BOOL, NSError *))completion;
        [Export ("fillLoginIntoWebView:forViewController:sender:completion:")]
        void FillLoginIntoWebView (NSObject webView, UIViewController viewController, NSObject sender, Action<bool, NSError> completion);
    }
}
