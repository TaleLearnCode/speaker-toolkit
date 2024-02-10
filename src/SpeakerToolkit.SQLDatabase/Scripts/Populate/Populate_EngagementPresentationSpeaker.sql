﻿SET IDENTITY_INSERT dbo.EngagementPresentationSpeaker ON;
GO

MERGE dbo.EngagementPresentationSpeaker AS TARGET
USING (VALUES (   1,    1, 1, 1),
              (   2,    2, 1, 1),
              (   3,    3, 1, 1),
              (   4,    4, 1, 1),
              (   5,    5, 1, 1),
              (   6,    6, 1, 1),
              (   7,    7, 1, 1),
              (   8,    8, 1, 1),
              (   9,    9, 1, 1),
              (  10,   10, 1, 1),
              (  11,   11, 1, 1),
              (  12,   12, 1, 1),
              (  13,   13, 1, 1),
              (  14,   14, 1, 1),
              (  15,   15, 1, 1),
              (  16,   16, 1, 1),
              (  17,   17, 1, 1),
              (  23,   23, 1, 1),
              (  24,   24, 1, 1),
              (  25,   25, 1, 1),
              (  26,   26, 1, 1),
              (  27,   27, 1, 1),
              (  28,   28, 1, 1),
              (  29,   29, 1, 1),
              (  30,   30, 1, 1),
              (  31,   31, 1, 1),
              (  32,   32, 1, 1),
              (  33,   33, 1, 1),
              (  34,   34, 1, 1),
              (  35,   35, 1, 1),
              (  36,   36, 1, 1),
              (  37,   37, 1, 1),
              (  38,   38, 1, 1),
              (  39,   39, 1, 1),
              (  40,   40, 1, 1),
              (  41,   41, 1, 1),
              (  42,   42, 1, 1),
              (  43,   43, 1, 1),
              (  44,   44, 1, 1),
              (  45,   45, 1, 1),
              (  46,   46, 1, 1),
              (  47,   47, 1, 1),
              (  48,   48, 1, 1),
              (  49,   49, 1, 1),
              (  50,   50, 1, 1),
              (  51,   51, 1, 1),
              (  52,   52, 1, 1),
              (  53,   53, 1, 1),
              (  54,   54, 1, 1),
              (  55,   55, 1, 1),
              (  56,   56, 1, 1),
              (  57,   57, 1, 1),
              (  58,   58, 1, 1),
              (  59,   59, 1, 1),
              (  60,   60, 1, 1),
              (  61,   61, 1, 1),
              (  62,   62, 1, 1),
              (  63,   63, 1, 1),
              (  64,   64, 1, 1),
              (  65,   65, 1, 1),
              (  66,   66, 1, 1),
              (  67,   67, 1, 1),
              (  68,   68, 1, 1),
              (  69,   69, 1, 1),
              (  70,   70, 1, 1),
              (  71,   71, 1, 1),
              (  72,   72, 1, 1),
              (  73,   73, 1, 1),
              (  74,   74, 1, 1),
              (  75,   75, 1, 1),
              (  76,   76, 1, 1),
              (  77,   77, 1, 1),
              (  78,   78, 1, 1),
              (  79,   79, 1, 1),
              (  80,   80, 1, 1),
              (  81,   81, 1, 1),
              (  82,   82, 1, 1),
              (  83,   83, 1, 1),
              (  84,   84, 1, 1),
              (  85,   85, 1, 1),
              (  86,   86, 1, 1),
              (  87,   87, 1, 1),
              (  88,   88, 1, 1),
              (  89,   89, 1, 1),
              (  90,   90, 1, 1),
              (  91,   91, 1, 1),
              (  92,   92, 1, 1),
              (  93,   93, 1, 1),
              (  94,   94, 1, 1),
              (  95,   95, 1, 1),
              (  96,   96, 1, 1),
              (  97,   97, 1, 1),
              (  98,   98, 1, 1),
              (  99,   99, 1, 1),
              ( 100,  100, 1, 1),
              ( 101,  101, 1, 1),
              ( 102,  102, 1, 1),
              ( 103,  103, 1, 1),
              ( 104,  104, 1, 1),
              ( 105,  105, 1, 1),
              ( 106,  106, 1, 1),
              ( 107,  107, 1, 1),
              ( 108,  108, 1, 1),
              ( 109,  109, 1, 1),
              ( 110,  110, 1, 1),
              ( 111,  111, 1, 1),
              ( 112,  112, 1, 1),
              ( 113,  113, 1, 1),
              ( 114,  114, 1, 1),
              ( 115,  115, 1, 1),
              ( 116,  116, 1, 1),
              ( 117,  117, 1, 1),
              ( 118,  118, 1, 1),
              ( 119,  119, 1, 1),
              ( 120,  120, 1, 1),
              ( 121,  121, 1, 1),
              ( 122,  122, 1, 1),
              ( 123,  123, 1, 1),
              ( 124,  124, 1, 1),
              ( 125,  125, 1, 1),
              ( 126,  126, 1, 1),
              ( 127,  127, 1, 1),
              ( 128,  128, 1, 1),
              ( 129,  129, 1, 1),
              ( 130,  130, 1, 1),
              ( 131,  131, 1, 1),
              ( 132,  132, 1, 1),
              ( 133,  133, 1, 1),
              ( 134,  134, 1, 1),
              ( 135,  135, 1, 1),
              ( 136,  136, 1, 1),
              ( 137,  137, 1, 1),
              ( 138,  138, 1, 1),
              ( 139,  139, 1, 1),
              ( 140,  140, 1, 1),
              ( 141,  141, 1, 1),
              ( 142,  142, 1, 1),
              ( 144,  144, 1, 1),
              ( 145,  145, 1, 1),
              ( 146,  146, 1, 1),
              ( 147,  147, 1, 1),
              ( 148,  148, 1, 1),
              ( 149,  149, 1, 1),
              ( 150,  150, 1, 1),
              ( 151,  151, 1, 1),
              ( 152,  152, 1, 1),
              ( 153,  153, 1, 1),
              ( 154,  154, 1, 1),
              ( 155,  155, 1, 1),
              ( 156,  156, 1, 1),
              ( 157,  157, 1, 1),
              ( 158,  158, 1, 1),
              ( 159,  159, 1, 1),
              ( 160,  160, 1, 1),
              ( 161,  161, 1, 1),
              ( 162,  162, 1, 1),
              ( 163,  163, 1, 1),
              ( 164,  164, 1, 1),
              ( 165,  165, 1, 1),
              ( 166,  166, 1, 1),
              ( 167,  167, 1, 1),
              ( 168,  168, 1, 1),
              ( 169,  169, 1, 1),
              ( 170,  170, 1, 1),
              (9001, 9001, 1, 1),
              (9154, 9154, 1, 1),
              (9155, 9155, 1, 1),
              (9168, 9168, 1, 1),
              (9169, 9169, 1, 1),
              (9170, 9170, 1, 1))
AS SOURCE (EngagementPresentationSpeakerId,
           EngagementPresentationId,
           SpeakerId,
           IsPrimarySpeaker)
ON TARGET.EngagementPresentationSpeakerId = SOURCE.EngagementPresentationSpeakerId
WHEN MATCHED THEN UPDATE SET EngagementPresentationId = SOURCE.EngagementPresentationId,
                             SpeakerId                = SOURCE.SpeakerId,
                             IsPrimarySpeaker         = SOURCE.IsPrimarySpeaker
WHEN NOT MATCHED THEN INSERT (EngagementPresentationSpeakerId,
                              EngagementPresentationId,
                              SpeakerId,
                              IsPrimarySpeaker)
                      VALUES (SOURCE.EngagementPresentationSpeakerId,
                              SOURCE.EngagementPresentationId,
                              SOURCE.SpeakerId,
                              SOURCE.IsPrimarySpeaker);
GO

SET IDENTITY_INSERT dbo.EngagementPresentationSpeaker OFF;
GO