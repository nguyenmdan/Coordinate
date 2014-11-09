using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media.Imaging;
using Microsoft.Phone.Tasks;
using System.Device.Location;

namespace Coordinate
{
    public partial class PhotoBrowser : PhoneApplicationPage
    {
        String[] flickrData;
        bool[] validImages;
        String[] imgURLs;
        int currentImage;
        public PhotoBrowser()
        {
            InitializeComponent();
            String imgURLsRaw = "http://farm7.staticflickr.com/6111/6224262742_b7951ec8c6_z.jpg;http://farm2.staticflickr.com/1436/1428800419_8a13bd4a70_z.jpg?zz=1;http://farm3.staticflickr.com/2534/3873160929_c1d1738135_z.jpg;http://farm8.staticflickr.com/7081/7351820378_66191c5eef_z.jpg;http://farm8.staticflickr.com/7130/7544624022_314cbf3fbf_z.jpg;http://farm8.staticflickr.com/7175/6761317911_a9ef75b9d0_z.jpg;http://farm3.staticflickr.com/2939/14499353789_6520f33fb9_z.jpg;http://farm6.staticflickr.com/5149/5701894637_a5532e9754_z.jpg;http://farm5.staticflickr.com/4010/4706842471_928a9a4f87_z.jpg;http://farm9.staticflickr.com/8099/8579229384_485bc1a691_z.jpg;http://farm8.staticflickr.com/7015/6743391127_e32bd6e422_z.jpg;http://farm6.staticflickr.com/5483/14135684129_a81b96dc99_z.jpg;http://farm8.staticflickr.com/7358/9737751966_5ab253bb47_z.jpg;http://farm1.staticflickr.com/208/474622677_9acff2b567_z.jpg;http://farm8.staticflickr.com/7071/7173320387_4f44f6dca4_z.jpg;http://farm4.staticflickr.com/3856/14551559503_72f977de81_z.jpg;http://farm8.staticflickr.com/7275/7146197881_64a7ec357f_z.jpg;http://farm8.staticflickr.com/7143/6743388089_9d6a388d5d_z.jpg;http://farm9.staticflickr.com/8071/8388114409_d2aac0f477_z.jpg;http://farm9.staticflickr.com/8356/8273230974_889ccdeb21_z.jpg;http://farm2.staticflickr.com/1367/1241422312_8852d342a2_z.jpg?zz=1;http://farm6.staticflickr.com/5160/5888995200_7780804375_z.jpg;http://farm4.staticflickr.com/3200/2487180002_edcab19951_z.jpg?zz=1;http://farm9.staticflickr.com/8234/8454028418_0dc4f293d5_z.jpg;http://farm2.staticflickr.com/1260/1322823803_e1d1206c70_z.jpg?zz=1;http://farm5.staticflickr.com/4092/5107197497_937d00d14c_z.jpg;http://farm3.staticflickr.com/2354/1626326028_891dd1ae99_z.jpg?zz=1;http://farm1.staticflickr.com/225/490935530_82d3124b05_z.jpg?zz=1;http://farm2.staticflickr.com/1227/1444160390_88acbf33cf_z.jpg?zz=1;http://farm6.staticflickr.com/5327/9316513497_4dfb8a9884_z.jpg;http://farm6.staticflickr.com/5158/14540765665_f615dbdb97_z.jpg;http://farm6.staticflickr.com/5343/7000111506_1c3d6602b8_z.jpg;http://farm8.staticflickr.com/7457/9736315525_d629de6c85_z.jpg;http://farm9.staticflickr.com/8262/8675644089_085911c20b_z.jpg;http://farm9.staticflickr.com/8112/8578118495_f8cb07407b_z.jpg;http://farm1.staticflickr.com/62/160661492_6c93e103d0_z.jpg;http://farm9.staticflickr.com/8237/8579227920_8b93945146_z.jpg;http://farm4.staticflickr.com/3906/14637748857_14ee52a0d9_z.jpg;http://farm9.staticflickr.com/8367/8579223776_a239d06a9a_z.jpg;http://farm3.staticflickr.com/2928/14035441873_084b459a25_z.jpg;http://farm8.staticflickr.com/7289/8729050141_823d5b8efd_z.jpg;http://farm9.staticflickr.com/8124/8671072961_4cabfdaeba_z.jpg;http://farm6.staticflickr.com/5327/7402255238_c132793d67_z.jpg;http://farm4.staticflickr.com/3882/14362038109_dba366a729_z.jpg;http://farm8.staticflickr.com/7173/6777384147_346f4ba8c1_z.jpg;http://farm1.staticflickr.com/26/51597920_1390676019_z.jpg?zz=1;http://farm8.staticflickr.com/7088/7188853337_45a2be278a_z.jpg;http://farm9.staticflickr.com/8392/8579238144_abfdba2d48_z.jpg;http://farm7.staticflickr.com/6020/6010870088_bfa1e679a0_z.jpg;http://farm6.staticflickr.com/5476/12040886736_0bd0eb6cb8_z.jpg;http://farm5.staticflickr.com/4079/4771587983_fb80d29b90_z.jpg;http://farm4.staticflickr.com/3147/3036299539_0692df06e6_z.jpg;http://farm9.staticflickr.com/8508/8466151390_21f3671f3f_z.jpg;http://farm9.staticflickr.com/8351/8450206895_e6b2f41908_z.jpg;http://farm6.staticflickr.com/5177/5399958229_8b3384219e_z.jpg;http://farm4.staticflickr.com/3679/11931341443_794b3462c1_z.jpg;http://farm8.staticflickr.com/7457/8729049155_e5f13a653e_z.jpg;http://farm4.staticflickr.com/3172/2489125792_a3565d3a5e_z.jpg;http://farm9.staticflickr.com/8526/8579225906_0ccdc23187_z.jpg;http://farm7.staticflickr.com/6008/6010808676_c5f58622cd_z.jpg;http://farm7.staticflickr.com/6145/6010250873_05b8cc1d9f_z.jpg;http://farm8.staticflickr.com/7022/6771484005_3de2c539fe_z.jpg;http://farm8.staticflickr.com/7166/6771481757_6babbcae8b_z.jpg;http://farm5.staticflickr.com/4112/5107825704_465b900ed7_z.jpg;http://farm7.staticflickr.com/6150/6010799932_b9a36d3a88_z.jpg;http://farm4.staticflickr.com/3056/2692720244_c23fedfe64_z.jpg;http://farm4.staticflickr.com/3291/2692717318_f7027a0eca_z.jpg;http://farm9.staticflickr.com/8006/7553286682_9e29784373_z.jpg;http://farm8.staticflickr.com/7450/9736248671_6f679941ae_z.jpg;http://farm9.staticflickr.com/8092/8579231662_d03b3bf884_z.jpg;http://farm9.staticflickr.com/8291/7553280606_fe93ae18f9_z.jpg;http://farm3.staticflickr.com/2018/5759198778_604562f8f0_z.jpg;http://farm9.staticflickr.com/8365/8501573662_c28dd687a1_z.jpg;http://farm4.staticflickr.com/3836/14824291415_8b71477955_z.jpg;http://farm8.staticflickr.com/7268/7130662937_9d3b1c66a2_z.jpg;http://farm6.staticflickr.com/5209/5284235865_d50b6a282e_z.jpg;http://farm7.staticflickr.com/6132/6010872700_1c7fb8ec7d_z.jpg;http://farm8.staticflickr.com/7425/8729038841_f82f2fe322_z.jpg;http://farm8.staticflickr.com/7392/8730167484_b45e6d30b2_z.jpg;http://farm1.staticflickr.com/22/35460703_4067568806.jpg;http://farm3.staticflickr.com/2502/3774120260_3322272fc5_z.jpg;http://farm4.staticflickr.com/3105/2663903657_c880a86b74_z.jpg;http://farm8.staticflickr.com/7101/6991201144_f04379f2bf_z.jpg;http://farm9.staticflickr.com/8334/8123948860_98166c908e_z.jpg;http://farm9.staticflickr.com/8300/7984406589_8e2fdb5d38_z.jpg;http://farm8.staticflickr.com/7274/7625279386_8475c8bd00_z.jpg;http://farm4.staticflickr.com/3777/9004295962_cab35c44c7_z.jpg;http://farm5.staticflickr.com/4144/5045338234_2005ef5eb5_z.jpg;http://farm7.staticflickr.com/6161/6166593875_54a5c748e4_z.jpg;http://farm4.staticflickr.com/3628/3637598821_d53f67cae3_z.jpg;http://farm4.staticflickr.com/3702/13083729324_e1029bdd1d_z.jpg;http://farm8.staticflickr.com/7301/11931080955_853fcb8f6a_z.jpg;http://farm1.staticflickr.com/62/201223327_0064dbd56e_z.jpg?zz=1;http://farm6.staticflickr.com/5136/5414131407_fdf5056e58_z.jpg;http://farm9.staticflickr.com/8175/7992943248_60eb0b3ecc_z.jpg;http://farm8.staticflickr.com/7348/11693043933_fe4182b27f_z.jpg;http://farm6.staticflickr.com/5230/5573633294_007df6dca9_z.jpg;http://farm4.staticflickr.com/3010/2664727170_acd52e0f6b_z.jpg;http://farm8.staticflickr.com/7451/8730157384_4e4d0d16d6_z.jpg;http://farm8.staticflickr.com/7316/9650627955_d8d08f07f7_z.jpg;http://farm2.staticflickr.com/1414/5122694188_113a24ef4e_z.jpg;http://farm1.staticflickr.com/71/201223328_d1a6bad969_z.jpg?zz=1;http://farm4.staticflickr.com/3096/2692721726_a3960d292c_z.jpg;http://farm9.staticflickr.com/8095/8578132763_dffa3af985_z.jpg;http://farm4.staticflickr.com/3337/3419902293_8400c0e1f2_z.jpg;http://farm9.staticflickr.com/8072/8344553012_2ea602a49e_z.jpg;http://farm6.staticflickr.com/5495/9422825588_63ff31cf46_z.jpg;http://farm3.staticflickr.com/2463/4433692180_98f85058be_z.jpg;http://farm4.staticflickr.com/3673/11745348174_7315a78e0a_z.jpg;http://farm5.staticflickr.com/4151/5042601231_dce2dcfdfe_z.jpg;http://farm4.staticflickr.com/3085/2691907309_49d42c59fb_z.jpg;http://farm5.staticflickr.com/4151/5042601231_dce2dcfdfe_z.jpg";
            imgURLs = imgURLsRaw.Split(new Char[]{';'});
            String rawData = "1428800419,34.020225,-118.284816,https://www.flickr.com/photos/12796946@N00/1428800419;3873160929,34.019514,-118.289966,https://www.flickr.com/photos/24518663@N08/3873160929;7351820378,34.021701,-118.285031,https://www.flickr.com/photos/53400673@N08/7351820378;7544624022,34.018358,-118.286225,https://www.flickr.com/photos/53400673@N08/7544624022;6761317911,34.021008,-118.282349,https://www.flickr.com/photos/53400673@N08/6761317911;14499353789,34.023533,-118.281404,https://www.flickr.com/photos/77799978@N00/14499353789;5701894637,34.02187,-118.286576,https://www.flickr.com/photos/94588149@N00/5701894637;4706842471,34.023925,-118.284988,https://www.flickr.com/photos/39525342@N04/4706842471;8579229384,34.020385,-118.285642,https://www.flickr.com/photos/12549675@N05/8579229384;6743391127,34.023409,-118.281226,https://www.flickr.com/photos/62837186@N06/6743391127;14135684129,34.022769,-118.281512,https://www.flickr.com/photos/92605333@N00/14135684129;9737751966,34.023532,-118.28582,https://www.flickr.com/photos/12549675@N05/9737751966;474622677,34.020688,-118.285503,https://www.flickr.com/photos/12796946@N00/474622677;7173320387,34.021328,-118.28266,https://www.flickr.com/photos/53400673@N08/7173320387;14551559503,34.021192,-118.284002,https://www.flickr.com/photos/92605333@N00/14551559503;7146197881,34.021227,-118.284112,https://www.flickr.com/photos/73417912@N00/7146197881;6743388089,34.023364,-118.281269,https://www.flickr.com/photos/62837186@N06/6743388089;8388114409,34.02051,-118.287906,https://www.flickr.com/photos/64065167@N04/8388114409;8273230974,34.020519,-118.285425,https://www.flickr.com/photos/54117860@N08/8273230974;1241422312,34.019585,-118.284988,https://www.flickr.com/photos/12796946@N00/1241422312;5888995200,34.021857,-118.280214,https://www.flickr.com/photos/34052289@N06/5888995200;2487180002,34.021079,-118.286361,https://www.flickr.com/photos/12796946@N00/2487180002;8454028418,34.021239,-118.280611,https://www.flickr.com/photos/39527581@N07/8454028418;1322823803,34.020794,-118.286705,https://www.flickr.com/photos/12796946@N00/1322823803;5107197497,34.021115,-118.288464,https://www.flickr.com/photos/12549675@N05/5107197497;1626326028,34.020368,-118.28516,https://www.flickr.com/photos/12796946@N00/1626326028;490935530,34.022039,-118.282027,https://www.flickr.com/photos/80658667@N00/490935530;1444160390,34.02179,-118.28722,https://www.flickr.com/photos/12796946@N00/1444160390;9316513497,34.019959,-118.286329,https://www.flickr.com/photos/92605333@N00/9316513497;14540765665,34.018874,-118.289108,https://www.flickr.com/photos/92605333@N00/14540765665;7000111506,34.021227,-118.284112,https://www.flickr.com/photos/73417912@N00/7000111506;9736315525,34.019905,-118.289977,https://www.flickr.com/photos/12549675@N05/9736315525;8675644089,34.021221,-118.28679,https://www.flickr.com/photos/54776562@N08/8675644089;8578118495,34.019184,-118.282562,https://www.flickr.com/photos/12549675@N05/8578118495;160661492,34.022502,-118.287391,https://www.flickr.com/photos/22243808@N00/160661492;8579227920,34.02131,-118.286372,https://www.flickr.com/photos/12549675@N05/8579227920;14637748857,34.020047,-118.283765,https://www.flickr.com/photos/8918304@N05/14637748857;8579223776,34.019994,-118.285793,https://www.flickr.com/photos/12549675@N05/8579223776;14035441873,34.021262,-118.283874,https://www.flickr.com/photos/92605333@N00/14035441873;8729050141,34.021364,-118.287048,https://www.flickr.com/photos/54776562@N08/8729050141;8671072961,34.021417,-118.287531,https://www.flickr.com/photos/64232630@N00/8671072961;7402255238,34.018482,-118.286597,https://www.flickr.com/photos/53400673@N08/7402255238;14362038109,34.022564,-118.286887,https://www.flickr.com/photos/92605333@N00/14362038109;6777384147,34.023587,-118.28144,https://www.flickr.com/photos/62837186@N06/6777384147;51597920,34.020901,-118.2834,https://www.flickr.com/photos/78467561@N00/51597920;7188853337,34.021026,-118.283475,https://www.flickr.com/photos/53400673@N08/7188853337;8579238144,34.019478,-118.283733,https://www.flickr.com/photos/12549675@N05/8579238144;6010870088,34.019549,-118.285889,https://www.flickr.com/photos/12549675@N05/6010870088;12040886736,34.020225,-118.285278,https://www.flickr.com/photos/51283825@N06/12040886736;4771587983,34.023596,-118.286202,https://www.flickr.com/photos/51421101@N02/4771587983;3036299539,34.023909,-118.280907,https://www.flickr.com/photos/7459168@N04/3036299539;8466151390,34.018803,-118.288807,https://www.flickr.com/photos/38243431@N03/8466151390;8450206895,34.021239,-118.280611,https://www.flickr.com/photos/39527581@N07/8450206895;5399958229,34.021043,-118.286533,https://www.flickr.com/photos/46728064@N06/5399958229;11931341443,34.021125,-118.284721,https://www.flickr.com/photos/12549675@N05/11931341443;8729049155,34.021435,-118.28679,https://www.flickr.com/photos/54776562@N08/8729049155;2489125792,34.020937,-118.285803,https://www.flickr.com/photos/67341227@N00/2489125792;8579225906,34.020848,-118.28575,https://www.flickr.com/photos/12549675@N05/8579225906;6010808676,34.020225,-118.285396,https://www.flickr.com/photos/12549675@N05/6010808676;6010250873,34.019184,-118.282562,https://www.flickr.com/photos/12549675@N05/6010250873;6771484005,34.023409,-118.28144,https://www.flickr.com/photos/62837186@N06/6771484005;6771481757,34.023524,-118.281397,https://www.flickr.com/photos/62837186@N06/6771481757;5107825704,34.021115,-118.288464,https://www.flickr.com/photos/12549675@N05/5107825704;6010799932,34.020368,-118.28458,https://www.flickr.com/photos/12549675@N05/6010799932;2692720244,34.020465,-118.284441,https://www.flickr.com/photos/45923298@N00/2692720244;2692717318,34.019185,-118.287777,https://www.flickr.com/photos/45923298@N00/2692717318;7553286682,34.020617,-118.285031,https://www.flickr.com/photos/8896423@N04/7553286682;9736248671,34.01971,-118.290213,https://www.flickr.com/photos/12549675@N05/9736248671;8579231662,34.021125,-118.284721,https://www.flickr.com/photos/12549675@N05/8579231662;7553280606,34.020617,-118.285031,https://www.flickr.com/photos/8896423@N04/7553280606;5759198778,34.02051,-118.285363,https://www.flickr.com/photos/51619679@N00/5759198778;8501573662,34.018349,-118.286114,https://www.flickr.com/photos/45923298@N00/8501573662;14824291415,34.022102,-118.283282,https://www.flickr.com/photos/8918304@N05/14824291415;7130662937,34.018416,-118.280611,https://www.flickr.com/photos/23820645@N05/7130662937;5284235865,34.020012,-118.289816,https://www.flickr.com/photos/24518663@N08/5284235865;6010872700,34.022533,-118.28678,https://www.flickr.com/photos/12549675@N05/6010872700;8729038841,34.021506,-118.28722,https://www.flickr.com/photos/54776562@N08/8729038841;8730167484,34.021719,-118.286962,https://www.flickr.com/photos/54776562@N08/8730167484;35460703,34.025347,-118.286533,https://www.flickr.com/photos/20738560@N00/35460703;3774120260,34.022666,-118.282834,https://www.flickr.com/photos/51035707567@N01/3774120260;2663903657,34.02083,-118.284569,https://www.flickr.com/photos/9902549@N08/2663903657;6991201144,34.01834,-118.288893,https://www.flickr.com/photos/30993133@N04/6991201144;8123948860,34.018287,-118.290878,https://www.flickr.com/photos/39017545@N02/8123948860;7984406589,34.023053,-118.288614,https://www.flickr.com/photos/60466607@N06/7984406589;7625279386,34.024138,-118.287466,https://www.flickr.com/photos/74494819@N05/7625279386;9004295962,34.021,-118.284,https://www.flickr.com/photos/77339424@N00/9004295962;5045338234,34.021897,-118.285675,https://www.flickr.com/photos/45534482@N03/5045338234;6166593875,34.020363,-118.283954,https://www.flickr.com/photos/45206671@N00/6166593875;3637598821,34.022502,-118.281089,https://www.flickr.com/photos/28423600@N02/3637598821;13083729324,34.020858,-118.287438,https://www.flickr.com/photos/97570635@N07/13083729324;11931080955,34.022351,-118.286672,https://www.flickr.com/photos/12549675@N05/11931080955;201223327,34.021924,-118.284065,https://www.flickr.com/photos/61737427@N00/201223327;5414131407,34.019821,-118.282424,https://www.flickr.com/photos/59031889@N06/5414131407;7992943248,34.022644,-118.281433,https://www.flickr.com/photos/31998658@N06/7992943248;11693043933,34.019,-118.286834,https://www.flickr.com/photos/25990689@N05/11693043933;5573633294,34.020617,-118.285406,https://www.flickr.com/photos/42513008@N07/5573633294;2664727170,34.021808,-118.283185,https://www.flickr.com/photos/9902549@N08/2664727170;8730157384,34.021933,-118.287048,https://www.flickr.com/photos/54776562@N08/8730157384;9650627955,34.0235,-118.2865,https://www.flickr.com/photos/39234410@N02/9650627955;5122694188,34.022075,-118.287048,https://www.flickr.com/photos/45534482@N03/5122694188;201223328,34.019403,-118.282005,https://www.flickr.com/photos/61737427@N00/201223328;2692721726,34.02195,-118.289741,https://www.flickr.com/photos/45923298@N00/2692721726;8578132763,34.018785,-118.284376,https://www.flickr.com/photos/12549675@N05/8578132763;3419902293,34.02195,-118.287112,https://www.flickr.com/photos/99323105@N00/3419902293;8344553012,34.022725,-118.28587,https://www.flickr.com/photos/43413770@N06/8344553012;9422825588,34.021728,-118.285868,https://www.flickr.com/photos/29929498@N00/9422825588;4433692180,34.021826,-118.286876,https://www.flickr.com/photos/45561728@N06/4433692180;11745348174,34.020305,-118.284234,https://www.flickr.com/photos/45206671@N00/11745348174;";
            flickrData = rawData.Split(new Char[] { ';' });
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string msg = "";
            if (NavigationContext.QueryString.TryGetValue("msg", out msg))
            {
                validImages = new bool[msg.Length];
                char[] paramPass = msg.ToCharArray();
                for (int i = 0; i < msg.Length; i++)
                {
                    if (paramPass[i] == '1')
                    {
                        validImages[i] = true;
                    }
                    else
                    {
                        validImages[i] = false;
                    }
                }
                int j = 0;
                while (!validImages[j] && j < validImages.Length-1)
                {
                    j++;
                    
                }
                currentImage = j;
                BitmapImage image = new BitmapImage(new Uri(imgURLs[j], UriKind.RelativeOrAbsolute));
                ImageBox.Source = image;
            }


        }

        private void buttonPrevious_Click(object sender, RoutedEventArgs e)
        {
           int j = currentImage;
           if (j > 0)
           {
               j--;
           }
           while (!validImages[j] && j > 0)
           {
               j--;
           }
           BitmapImage image = new BitmapImage(new Uri(imgURLs[j], UriKind.RelativeOrAbsolute));
           ImageBox.Source = image;
           currentImage = j;
        }

        private void buttonNext_Click(object sender, RoutedEventArgs e)
        {
            int j = currentImage;
            if (j < validImages.Length - 1)
            {
                j++;
            }
            while (!validImages[j] && j < validImages.Length-1)
            {
                j++;
            }
            BitmapImage image = new BitmapImage(new Uri(imgURLs[j], UriKind.RelativeOrAbsolute));
            ImageBox.Source = image;
            currentImage = j;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            String selectedImage = flickrData[currentImage];
            String[] metadata;
            metadata = selectedImage.Split(new Char[]{','});
            String latitude = metadata[1];
            String longitude = metadata[2];
            BingMapsDirectionsTask bingMapsDirectionsTask = new BingMapsDirectionsTask();
            GeoCoordinate coordinates = new GeoCoordinate(Double.Parse(latitude), Double.Parse(longitude));
            LabeledMapLocation destination = new LabeledMapLocation("Photo Coordinate", coordinates);
            bingMapsDirectionsTask.End = destination;
            bingMapsDirectionsTask.Show();
        }
    }
}